using FS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FS.Controllers {

    public class HomeController : Controller {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(SignInManager<AppUser> signInManager,

            UserManager<AppUser> userManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //public IActionResult Index() {
        //  return View();
        //  }
        public IActionResult Index() {
            if(_signInManager.IsSignedIn(User)) {
                if(User.IsInRole("Admin"))
                    return Redirect("/Admin/Classes");
                if(User.IsInRole("Trainer"))
                    return Redirect("/Admin/Classes");
                if(User.IsInRole("Trainee"))
                    return Redirect("/Admin/Classes");
            }
            return Redirect("/identity/account/login");
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}