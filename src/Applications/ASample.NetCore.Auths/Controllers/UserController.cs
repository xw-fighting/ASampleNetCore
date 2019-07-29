﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASample.NetCore.Auths.Models;
using Microsoft.AspNetCore.Authorization;

namespace ASample.NetCore.Auths.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
