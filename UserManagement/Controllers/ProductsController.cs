using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Constants;

namespace UserManagement.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Permissions.Products.Edit)]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
