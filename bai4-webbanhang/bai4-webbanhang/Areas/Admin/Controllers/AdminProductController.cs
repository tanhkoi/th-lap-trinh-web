using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bai4_webbanhang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProductController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
