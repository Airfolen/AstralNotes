//using System.Threading.Tasks;
//using AstralNotes.Domain.Authentication;
//using AstralNotes.Domain.Authentication.Models;
//using Microsoft.AspNetCore.Mvc;
//
//namespace AstralNotes.API.Controllers
//{
//    [Route("Authorization")]
//    public class AuthorizationController : Controller
//    {
//        private readonly IAuthenticationService _authenticationService;
//
//        public AuthorizationController(IAuthenticationService authenticationService)
//        {
//            _authenticationService = authenticationService;
//        }
//
//        /// <summary>
//        /// Аутентификация пользоваетеля
//        /// </summary>
//        /// <param name="model">Модель аутентификации</param>
//        /// <returns>Результат авторизации в виде токена</returns>
//        [Route("SignIn")]
//        [HttpPost]
//        public async Task<AuthenticationResult> SignIn([FromBody] SignInModel model)
//        {
//            return await _authenticationService.AuthenticateUser(model);
//        }
//    }
//}