using Microsoft.AspNetCore.Mvc;

namespace API_MSSQL.Controllers
{
    public class EntiriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
