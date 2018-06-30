using System.Threading.Tasks;
using AstralNotes.Database.Entities;
using AstralNotes.Domain.Authentication.Models;
using AstralNotes.Domain.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AstralNotes.API.Controllers
{
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
        
        [HttpGet]
        [Route("Registration")]
        public IActionResult Registration()
        {
            return View();
        }
        
      
        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration(UserInfo model)
        {
            if(ModelState.IsValid)
            {
                User user = new User { Login = model.Login, UserName = model.UserName};
            
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
        /// Аутентификация пользоваетеля
        /// </summary>
        /// <param name="returnUrl">Адрес для возврата</param>
        [HttpGet]
        [Route("Login")]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new SignInModel { ReturnUrl = returnUrl });
        }
 
        /// <summary>
        /// Аутентификация пользоваетеля
        /// </summary>
        /// <param name="model">Модель аутентификации</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Login")]
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
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}