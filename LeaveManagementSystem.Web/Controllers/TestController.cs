using LeaveManagementSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Web.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            var data = new TestViewModel
            {
                Name = "Student of Mvc Mastery",
                Id = 1,
                DateOfBirth = new DateTime(1997,12,04)

            };
            return View(data);
        }
    }
}
