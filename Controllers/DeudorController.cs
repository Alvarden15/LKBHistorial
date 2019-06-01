using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using Microsoft.AspNetCore.Authorization;


namespace LKBHistorial.Controllers
{
    [Authorize("LKB Historial")]
    public class DeudorController:Controller
    {
        private readonly MvcContext _context;

        public DeudorController(MvcContext context){
            _context=context;
        }

        public void ListadoPerros(){
            var perros= _context.Perro.AsNoTracking().ToList();
            ViewBag.Perros=new SelectList(perros,"Id","Nombre");
        }

        public IActionResult RegistrarDeudor(){

            return View();
        }

        public async Task<IActionResult> ListaDeudores(){
            var deudor= from m in _context.Deudor select m;

            return View(await deudor.AsNoTracking().ToListAsync());
        }

        public IActionResult Estadisticas(){

            return View();
        }


        public IActionResult ModificarDeudor(){

            return View();
        }

        public IActionResult ConfirmacionDeudor(){

            return RedirectToAction("ListaDeudores");
        }




        
        
    }
}