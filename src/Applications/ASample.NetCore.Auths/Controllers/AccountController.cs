using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.Auths.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Password()
        {
            return View();
        }
    }
}