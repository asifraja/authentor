﻿using Authentor.IdentityData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentor.Controllers
{
    // Checkout https://www.youtube.com/watch?v=n-g9O0dOV9A&t=21s by Raw Coding. 
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {

            var user = await _userManager.FindByNameAsync(username);
            if(user!=null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                {
                    User.Claims.Append(new System.Security.Claims.Claim("Claim.DoB", "12/12/2019"));
                }
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = ""
            };
            
            var result = await _userManager.CreateAsync(user,password);

            if(result.Succeeded)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                {
                    User.Claims.Append(new System.Security.Claims.Claim("Claim.DoB", "12/12/2019"));
                }
            }
            return RedirectToAction("index");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        //// Example of using authorization Service logically within the action or method
        //public async Task<IActionResult> DoStuffWithContructorIoC()
        //{
        //    //await _authorizationService.AuthorizeAsync(HttpContext.User, "Claim.DoB");
        //    // or
        //    var result = await _authorizationService.AuthorizeAsync(User, "Claim.DoB");
        //    if(result.Succeeded)
        //    {
        //        return View("Secret");
        //    }
        //    return View("Index");
        //}
        
        //// Example of using authorization Service logically within the action or method
        //public async Task<IActionResult> DoStuffWithMethodIoC([FromServices] IAuthorizationService authorizationService)
        //{
        //    //await _authorizationService.AuthorizeAsync(HttpContext.User, "Claim.DoB");
        //    // or
        //    var result = await authorizationService.AuthorizeAsync(User, "Claim.DoB");
        //    if (result.Succeeded)
        //    {
        //        return View("Secret");
        //    }
        //    return View("Index");
        //}
    }
}
