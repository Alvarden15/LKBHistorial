using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
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
    public class HembraController:Controller
    {
        private MvcContext _context;

        public HembraController(MvcContext hc){
            _context=hc;
        }
        
         public void ListadoHembras(){
            var hembras=_context.Perro.AsNoTracking().Where(m=>m.Sexo.Contains("H"));
            ViewBag.Hembras=new SelectList(hembras,"Id","Nombre");
        }

        public void ListadoMachos(){
            var machos=_context.Perro.AsNoTracking().Where(o=>o.Sexo.Contains("M"));
            ViewBag.Machos=new SelectList(machos,"Id","Nombre");
        }
        [Authorize("LKB Historial")]
          public IActionResult RegistrarLunada(){
            ListadoHembras();
            return View();
        }
        [Authorize("LKB Historial")]
        public IActionResult RegistrarPrenada(){
            ListadoHembras();
            return View();
        }

        public IActionResult ConfirmacionPrenada(){
            return RedirectToAction("ListaPrenada");
        }
        public IActionResult ConfirmacionLunada(){
            return RedirectToAction("ListaLunada");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarPrenada([Bind("CantidadInseminadas,FechaInicio,FechaFin,TipoParto,FechaCesaria,FechaPartoNormal,NumeroCamadas,IdPerro")]Prenada prenada){
            if(ModelState.IsValid &&(prenada.FechaFin>prenada.FechaInicio)){
                _context.Prenada.Add(prenada);
                await _context.SaveChangesAsync();
                return RedirectToAction("ConfirmacionPrenada");
            }

            if(prenada.FechaFin<=prenada.FechaInicio){
                ModelState.AddModelError(string.Empty,"La fecha final debe ser superior a la fecha de inicio");
            }
            ListadoHembras();
            return View(prenada);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarLunada([Bind("Id,FechaInicio,FechaFin,NumeroCelos,IdPerro")]Lunada lunada){

            if(ModelState.IsValid &&(lunada.FechaFin>lunada.FechaInicio)){
                _context.Lunada.Add(lunada);
                await _context.SaveChangesAsync();
                return RedirectToAction("ConfirmacionLunada");
            }

            if(lunada.FechaFin<=lunada.FechaInicio){
                ModelState.AddModelError(string.Empty,"La fecha final debe ser superior a la fecha de inicio");

            }


            ListadoHembras();
            return View(lunada);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ListaPrenada(){
            
            return View(await _context.Prenada.AsNoTracking().Include(p=>p.Perro).ToListAsync());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ListaLunada(){
          
            return View(await _context.Lunada.AsNoTracking().Include(p=>p.Perro).ToListAsync());
        }



    }
}