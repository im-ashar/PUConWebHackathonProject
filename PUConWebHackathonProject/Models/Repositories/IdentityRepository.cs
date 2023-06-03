using Microsoft.AspNetCore.Identity;
using PUConWebHackathonProject.Models.IRepositories;
using PUConWebHackathonProject.Models.Repositories.Identity;

namespace PUConWebHackathonProject.Models.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<IdentityModel> _userManager;
        private readonly SignInManager<IdentityModel> _signInManager;

        public IdentityRepository(UserManager<IdentityModel> userManager, SignInManager<IdentityModel> sManager)
        {
            _userManager = userManager;
            _signInManager = sManager;
        }

        public async Task<IdentityResult> SignUp(IdentityModel model)
        {
            var user = new IdentityModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.User_Name,
                Email = model.User_Email,
                EmailConfirmed = true,
                LockoutEnabled = false,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return result;
        }

        public async Task<SignInResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.User_Name);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                return result;
            }
            return null;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            return;
        }
    }
}
