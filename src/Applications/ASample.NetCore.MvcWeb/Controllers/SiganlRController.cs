using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.MvcWeb.Controllers
{
    public class SiganlRController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}