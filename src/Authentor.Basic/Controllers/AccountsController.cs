using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Authentor.Basic.Controllers
{
    public class AccountsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}