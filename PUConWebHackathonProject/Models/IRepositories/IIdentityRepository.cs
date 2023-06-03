using Microsoft.AspNetCore.Identity;
using PUConWebHackathonProject.Models.Repositories;
using PUConWebHackathonProject.Models.Repositories.Identity;

namespace PUConWebHackathonProject.Models.IRepositories
{
    public interface IIdentityRepository
    {
        Task<IdentityResult> SignUp(IdentityModel model);
        Task<SignInResult> Login(LoginModel model);
        Task Logout();
    }
}
