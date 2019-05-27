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

          public IActionResult RegistrarLunada(){
            ListadoHembras();
            return View();
        }

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
        public async Task<IActionResult> RegistrarPrenada([Bind("Id,CantidadInseminadas,FechaInicio,FechaFin,TipoParto,FechaCesaria,FechaPartoNormal,NumeroCamadas,IdPerro")]Prenada prenada){
            if(ModelState.IsValid){
                _context.Prenada.Add(prenada);
                await _context.SaveChangesAsync();
                return RedirectToAction("ConfirmacionPrenada");
            }
            ListadoHembras();
            return View(prenada);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarLunada([Bind("Id,FechaInicio,FechaFin,NumeroCelos,IdPerro")]Lunada lunada){
            if(ModelState.IsValid){
                _context.Lunada.Add(lunada);
                await _context.SaveChangesAsync();
                return RedirectToAction("ConfirmacionLunada");
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