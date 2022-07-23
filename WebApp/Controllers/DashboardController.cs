using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public DashboardController(IWebHostEnvironment env)
        {
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserList()
        {

            return View();
        }

        public IActionResult EditUser(int id)
        {
            return View();
        }

        public IActionResult ReportList()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadFile()
        {
            foreach (var formFile in Request.Form.Files)
            {
                var fulPath = Path.Combine(_env.ContentRootPath, "wwwroot\\images", formFile.FileName);
                using (FileStream fs = System.IO.File.Create(fulPath))
                {
                    formFile.CopyTo(fs);
                    fs.Flush();
                }
                return Json(Request.Form.Files[0].FileName);
            }
            return Json("Please Try Again !!");
        }





    }
}
