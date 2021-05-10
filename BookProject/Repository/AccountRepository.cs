using System;
using System.Threading.Tasks;
using BookProject.Data;
using BookProject.Models;
using BookProject.Service;
using Microsoft.AspNetCore.Identity;

namespace BookProject.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserService userService)
        {
            this._userService = userService;
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel signUpModel)
        {
            var user = new ApplicationUser()
            {
                Email = signUpModel.Email,
                UserName = signUpModel.Email,
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName
            };
            if (signUpModel.DateOfBirth != null)
            {
                user.DateOfBirth = signUpModel.DateOfBirth;
            }
            else
            {
                user.DateOfBirth = DateTime.UtcNow;
            }
            return await this._userManager.CreateAsync(user, signUpModel.Password);
        }
        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            return await this._signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);
        }
    }
}
