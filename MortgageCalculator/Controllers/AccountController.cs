﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MortgageCalculator.Models;


namespace MortgageCalculator.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public AccountController() { }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return new CustomJson(new
                    {
                        success = true,
                    }, JsonRequestBehavior.DenyGet);
                default:
                    return new CustomJson(new
                    {
                        success = false,
                        message = "Invalid login attempt."
                    }, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var message = string.Empty;
                foreach (var resultError in result.Errors)
                {
                    message += $"\n{resultError}";
                }
                return new CustomJson(new
                {
                    success = false,
                    message = message
                }, JsonRequestBehavior.DenyGet);
            }
                
            await SignInManager.SignInAsync(user, false, false);
            return new CustomJson(new
            {
                success = true,
            }, JsonRequestBehavior.DenyGet);
        }
    }
}