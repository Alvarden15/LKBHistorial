using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LKBHistorial.Models;
using Microsoft.AspNetCore.Authorization;


namespace LKBHistorial.Controllers
{
    [Authorize("LKB Historial")]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        
        [Authorize("LKB Historial")]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact(){
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewData["statusCode"] = HttpContext.Response.StatusCode;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
