using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LKBHistorial.Models;
using Models.MvcContext;
using Microsoft.EntityFrameworkCore;



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

        public IActionResult ListaDeudor(){

            return View();
        }

        public IActionResult Estadisticas(){

            return View();
        }


        public IActionResult ModificarDeudor(){

            return View();
        }





        
        
    }
}