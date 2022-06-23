using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrChronoWebAPI.Controllers
{
    public class ValueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
