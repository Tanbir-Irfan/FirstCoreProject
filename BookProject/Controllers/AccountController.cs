using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProject.Models;
using BookProject.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookProject.Controllers
{
    [Route("[Controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        [Route("/Account/{action?}")]
        public ViewResult SignUp()
        {
            return View();
        }

        [Route("/Account/{action?}")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel signUpUserModel)
        {
            if (ModelState.IsValid)
            {
                var result = await this._accountRepository.CreateUserAsync(signUpUserModel);
                if(!result.Succeeded){
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    ModelState.Clear();
                }
            }
            return View();
        }

        [Route("/Account/{action}")]
        public ViewResult SignIn()
        {
            return View();
        }

        [Route("/Account/{action}"), HttpPost]
        public async Task<IActionResult> SignIn(SignInModel signInModel, string returnURL)
        {
            if(ModelState.IsValid)
            {
                var result = await this._accountRepository.PasswordSignInAsync(signInModel);
                if(result.Succeeded){
                    // ModelState.Clear();
                    if(!string.IsNullOrEmpty(returnURL))
                    {
                        return LocalRedirect(returnURL);
                    }
                    return RedirectToAction("Index", "Home");
                }
                if(result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Not Allowed To Sign In");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }
            }
            return View();
        }

        [Route("SingOut")]
        public async Task<IActionResult> SingOut()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Route("Change-Password")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost("Change-Password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountRepository.ChangePasswordAsync(model);
                if(result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return View();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }


    }
}
