using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AstralNotes.Utils.Filters
{
    /// <summary>
    /// Обработчик ошибок
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = context.HttpContext.Response;
            switch (context.Exception)
            {
                case NotImplementedException err:
                    response.StatusCode = (int) HttpStatusCode.NotImplemented;
                    break;
                default:
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
            }
        
            var content = new
            {
                message = context.Exception.Message
            };
            response.ContentType = "application/json";
            context.Result = new JsonResult(content);
        }
    }
}
