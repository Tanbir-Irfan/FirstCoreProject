using System.Threading.Tasks;
using BookProject.Models;
using Microsoft.AspNetCore.Identity;

namespace BookProject.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel);
        Task<IdentityResult> CreateUserAsync(SignUpUserModel signUpModel);
        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);
        Task SignOutAsync();
    }
}