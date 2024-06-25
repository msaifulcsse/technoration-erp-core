using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class StudentDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyProfile()
        {
            return View();
        }
    }
}
