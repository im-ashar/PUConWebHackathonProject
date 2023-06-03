using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PUConWebHackathonProject.Models;
using PUConWebHackathonProject.Models.IRepositories;
using PUConWebHackathonProject.Models.Repositories;
using PUConWebHackathonProject.Models.Repositories.Identity;

namespace PUConWebHackathonProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityRepository _identityRepository;
        public AccountController(IIdentityRepository userRepo)
        {
            _identityRepository = userRepo;
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(IdentityModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityRepository.SignUp(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityRepository.Login(model);
                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Username or Password");
                    return View(model);
                }
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Username or Password");
            return View(model);
        }
    }
}
