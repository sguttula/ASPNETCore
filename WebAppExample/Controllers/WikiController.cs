using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAppExample.Controllers
{
    public class WikiController : Controller
    {
        public IActionResult Index(string path)
        {
            return View("Index", path);
        }
    }
}
