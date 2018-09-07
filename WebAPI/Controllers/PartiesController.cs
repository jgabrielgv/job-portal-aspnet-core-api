using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Core;
using WebAPI.Core.Domain;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Authorize]
    public class PartiesController : BaseController
    {
        public PartiesController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
            : base(unitOfWork, mapper, userManager)
        {
        }

        public async Task<IActionResult> GetllAsync() => Ok(await UnitOfWork.Parties.GetAllAsync());

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Company))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostAsync([FromBody] CreatePartyDTO party)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Party entity = Mapper.Map<CreatePartyDTO, Party>(party);

            await UnitOfWork.Parties.AddAsync(entity);
            await UnitOfWork.CompleteAsync();
            return new CreatedResult("/api/parties", entity);
        }

        [HttpPut]
        [ProducesResponseType(200, Type= typeof(Party))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutAsync([FromBody] UpdatePartyDTO party)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(!await UnitOfWork.Parties.AnyAsync(e => e.PartyId == party.PartyId))
            {
                return NotFound();
            }

            Party entity = await UnitOfWork.Parties.SingleOrDefaultAsync(p => p.PartyId == party.PartyId);
            entity.LastName = party.LastName;
            entity.FirstName = party.FirstName;
            entity.SecondLastName = party.SecondLastName;
            entity.Type = char.Parse(party.Type);
            await UnitOfWork.CompleteAsync();

            return Ok(entity);
        }

    }
}