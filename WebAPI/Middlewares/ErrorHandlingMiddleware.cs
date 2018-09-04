using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace WebAPI.Middlewares {
    public class ErrorHandlingMiddleware {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware (RequestDelegate next) {
            this.next = next;
        }

        public async Task Invoke (HttpContext context /* other dependencies */ ) {
            try {
                await next (context);
            } catch (Exception ex) {
                await HandleExceptionAsync (context, ex);
            }
        }

        private static Task HandleExceptionAsync (HttpContext context, Exception exception) {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            string errorMessage = exception.Message;

            // manage and log custom validations
            if (exception is InvalidOperationException
                || exception is DbUpdateException
                || exception is DbUpdateConcurrencyException) {
                    errorMessage = "An exception has occured while saving the data. Please try again.";
                }
            //if (exception is MyNotFoundException) code = HttpStatusCode.NotFound;
            //else if (exception is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (exception is MyException) code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject (new { error = errorMessage });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            return context.Response.WriteAsync (result);
        }
    }
}