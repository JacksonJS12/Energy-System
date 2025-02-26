namespace EnergySystem.Web.Controllers
{
    using System;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using EnergySystem.Data.Models;
    using EnergySystem.Web.ViewModels.User;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UserController : BaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpClient _httpClient;

        public UserController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IHttpClientFactory httpClientFactory)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            // ** Check if the password is compromised **
            if (await IsPasswordBreached(model.Password))
            {
                this.ModelState.AddModelError(string.Empty, "This password has been found in a data breach. Please choose a different one.");
                return View(model);
            }

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            await this._signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View(new LoginFormModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            var result = await this._signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            return Redirect(model.ReturnUrl ?? "/Home/Index");
        }

        // ** Helper Method to Check if Password is Breached **
        private async Task<bool> IsPasswordBreached(string password)
        {
            using var sha1 = SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashString = BitConverter.ToString(hash).Replace("-", "").ToUpper();
            var prefix = hashString.Substring(0, 5);
            var suffix = hashString.Substring(5);

            var response = await this._httpClient.GetStringAsync($"https://api.pwnedpasswords.com/range/{prefix}");
            return response.Contains(suffix);
        }
    }
}
