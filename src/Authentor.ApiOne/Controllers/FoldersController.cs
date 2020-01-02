using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentor.ApiOne.Controllers
{
    public class FoldersController : Controller
    {
        [HttpGet]
        [Route("/secret")]
        [Authorize]
        public async Task<string> Get()
        {
            return await Task.FromResult("Secret Folder One");
        }
    }
}
