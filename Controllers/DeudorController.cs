using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LKBHistorial.Models;
using Microsoft.EntityFrameworkCore;
using Models.MvcContext;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace LKBHistorial.Controllers
{
    public class DeudorController:Controller
    {
        private readonly MvcContext _context;

        public DeudorController(MvcContext context){
            _context=context;
        }

        public IActionResult RegistrarDeudor(){

            return View();
        }

        public IActionResult ListadoDeudor(){

            return View();
        }

        public IActionResult Estadisticas(){

            return View();
        }


        public IActionResult ModificarDeudor(){

            return View();
        }

        public IActionResult ConfirmacionDeudor(){

            return View();
        }





        
        
    }
}