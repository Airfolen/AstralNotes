using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AstralNotes.API.Controllers
{
    [Route("Test")]
    public class TestController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;

        public TestController(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        [HttpGet]
        public object GetInfo()
        {
            var claims = _httpContext.HttpContext.User.Claims.ToString();
            var identities = _httpContext.HttpContext.User.Identities.ToString();
            var name = _httpContext.HttpContext.User.Identity.Name;
            var authenticationType = _httpContext.HttpContext.User.Identity.AuthenticationType;

            return new {Claims = claims, Identities = identities, Name = name, AuthenticationType = authenticationType};
        }
    }
}