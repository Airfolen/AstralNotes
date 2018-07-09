using System.Threading.Tasks;
using AstralNotes.Database.Entities;
using AstralNotes.Domain.Authentication.Models;
using AstralNotes.Domain.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AstralNotes.API.Controllers
{
    /// <summary>
    /// API для регистрации и авторизации пользователей
    /// </summary>
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
 
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        [HttpGet]
        [Route("Registration")]
        public IActionResult Registration()
        {
            return View();
        }
        
        /// <summary>
        /// Регистрация пользователя
        /// <param name="model">Входная модель пользователя</param>
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("Registration")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(UserInfo model)
        {
            if(ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Login, 
                    FullName = model.FullName
                };
            
                var result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            
            return View(model);
        }
        
        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="returnUrl">Адрес для возврата</param>
        [HttpGet]
        [Route("Login")]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
              
            return View(new SignInModel { ReturnUrl = returnUrl });
        }
 
        /// <summary>
        /// Аутентификация пользоваетеля
        /// </summary>
        /// <param name="model">Модель аутентификации</param>
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                var result = 
                    await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
                
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            
            return View(model);
        }
 
        /// <summary>
        /// Выход из учетной записи пользователя
        /// </summary>  
        [HttpGet]
        [Authorize]
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            
            return RedirectToAction("Index", "Home");
        }
    }
}