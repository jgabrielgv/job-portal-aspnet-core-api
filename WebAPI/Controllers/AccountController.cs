using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPI.DTOs;
using WebAPI.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IAppSettings _appSettings;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IUnitOfWork unitOfWork,
            IAppSettings appSettings
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _appSettings = appSettings;
            UnitOfWork = unitOfWork;
        }

        [Route("login")]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDTO model)
        {
            if (!this.ValidateLogin(model, ModelState))
            {
                return BadRequest(ModelState);
            }

            if (model.Grant_Type == "password")
            {
                return await PasswordLogin(model, ModelState);
            }
            else if (model.Grant_Type == "refresh_token")
            {
                return await RefreshUserTokenLogin(model, ModelState);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        [Route("register")]
        [HttpPost]
        public async Task<object> Register([FromBody] RegisterDTO model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                var refreshToken = GenerateRefreshToken();

                RefreshUserToken userToken = CreateRefreshUserToken(refreshToken, user);
                await UnitOfWork.UserTokens.AddAsync(userToken);
                await UnitOfWork.CompleteAsync();

                return new
                {
                    token = await GenerateJwtToken(model.Email, user),
                    refresh_token = refreshToken
                };
            }
            else
            {
                return BadRequest(result.Errors.Any() ? result.Errors.First().Description : "Invalid signup. Please try again."); // fix this
            }
            throw new ApplicationException("UNKNOWN_ERROR");
        }

        protected virtual async Task<IActionResult> PasswordLogin(LoginDTO login, ModelStateDictionary modelState)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

            if (result.Succeeded)
            {
                string refreshToken = GenerateRefreshToken();
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == login.Email);

                RefreshUserToken userToken = CreateRefreshUserToken(refreshToken, appUser);
                await UnitOfWork.UserTokens.AddAsync(userToken);
                await UnitOfWork.CompleteAsync();

                return Ok(new
                {
                    token = await GenerateJwtToken(login.Email, appUser),
                    refresh_token = refreshToken
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        protected virtual async Task<IActionResult> RefreshUserTokenLogin(LoginDTO login, ModelStateDictionary modelState)
        {
            RefreshUserToken existingToken = await UnitOfWork.UserTokens.SingleOrDefaultAsync(p => p.RefreshToken == login.Refresh_Token);
            if (existingToken == null)
            {
                return Unauthorized();
            }

            ApplicationUser appUser = _userManager.Users.SingleOrDefault(e => e.Id == existingToken.UserId);
            if (appUser == null)
            {
                return NotFound();
            }

            UnitOfWork.UserTokens.Remove(existingToken);
            string refreshToken = GenerateRefreshToken();
            await UnitOfWork.UserTokens.AddAsync(CreateRefreshUserToken(refreshToken, appUser));
            await UnitOfWork.CompleteAsync();

            return Ok(new
            {
                token = await GenerateJwtToken(existingToken.User.Email, appUser),
                refresh_token = refreshToken
            });
        }

        protected virtual RefreshUserToken CreateRefreshUserToken(string refreshToken, ApplicationUser appUser) => new RefreshUserToken
        {
            RefreshUserTokenId = Guid.NewGuid().ToString(),
            RefreshToken = refreshToken,
            UserId = appUser.Id
        };

        protected virtual string GenerateRefreshToken() => Guid.NewGuid().ToString().Replace("-", "");

        protected virtual async Task<object> GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.ApiJwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtExpireMinutes"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token));
        }

        protected virtual bool ValidateLogin(LoginDTO login, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return modelState.IsValid;
            }
            if (login.Grant_Type != "password" && login.Grant_Type != "refresh_token")
            {
                modelState.AddModelError("GrantType", "Invalid Grant Type");
            }
            if (login.Grant_Type == "password")
            {
                if (string.IsNullOrEmpty(login.Email))
                {
                    modelState.AddModelError("Email", "Email field is required");
                }
                if (string.IsNullOrEmpty(login.Password))
                {
                    modelState.AddModelError("Password", "Password field is required");
                }
            }
            else if (login.Grant_Type == "refresh_token")
            {
                if (string.IsNullOrEmpty(login.Refresh_Token))
                {
                    modelState.AddModelError("Refresh_Token", "Refresh_Token field is required");
                }
            }
            return modelState.IsValid;
        }

        public IUnitOfWork UnitOfWork { get; private set; }
    }
}