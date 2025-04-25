using System.Net;
using System.Text.Json;
using Domain.Exceptions;
using E_Commerce.Middlewares;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            this._Next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext Request)
        {
            try
            {

                await _Next.Invoke(Request);

                await HandleNotFoundEndPointAsync(Request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Wrong");

                await HandleExceptionAsync(Request, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext Request, Exception ex)
        {
            Request.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            Request.Response.ContentType = "application/json";

            var response = new ErrorDetails
            {
                //StatusCode = (int)HttpStatusCode.InternalServerError,
                ErroeMsg = ex.Message
            };
            response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException=> StatusCodes.Status401Unauthorized,
                BadRequestException BadRequest=>GetValidationErrors(BadRequest,response),
                _ => StatusCodes.Status500InternalServerError
            };
            Request.Response.StatusCode = response.StatusCode;
            var JsonResult = JsonSerializer.Serialize(response);

            await Request.Response.WriteAsync(JsonResult);
        }

        private static int GetValidationErrors(BadRequestException badRequest, ErrorDetails response)
        {
                response.Errors=badRequest.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext Request)
        {
            if (Request.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                Request.Response.ContentType = "application/json";

                var response = new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ErroeMsg = $"End Point {Request.Request.Path} Not Found"
                };

                await Request.Response.WriteAsJsonAsync(response);

            }
        }
    }
}


public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandlerMiddleWare(this IApplicationBuilder app)
    {
        app.UseMiddleware<CustomExceptionHandlerMiddleware>();
        return app;
    }
}