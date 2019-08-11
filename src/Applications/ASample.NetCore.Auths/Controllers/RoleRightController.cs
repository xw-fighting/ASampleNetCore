using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ASample.NetCore.Auths.Controllers
{
    [Authorize]
    public class RoleRightController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
