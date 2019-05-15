using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
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

namespace LKBHistorial.Controllers
{
    public class HembraController:Controller
    {
        private MvcContext _context;

        public HembraController(MvcContext hc){
            _context=hc;
        }
         public void ListadoHembras(){
            var hembras=_context.Perro.Where(m=>m.Sexo.Contains("H"));
            ViewBag.Hembras=new SelectList(hembras,"Id","Nombre");
        }

        public void ListadoMachos(){
            var machos=_context.Perro.Where(o=>o.Sexo.Contains("M"));
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
        public async Task<IActionResult> RegistrarPrenada([Bind("Id,CantidadInseminada,FechaInicio,FechaFin,TipoParto,FechaCesaria,FechaPartoNormal,NumeroCamadas,IdPerro")]Prenada prenada){
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
        public async Task<IActionResult> ListaPrenada(){
            return View(await _context.Prenada.AsNoTracking().ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> ListaLunada(){
            return View(await _context.Lunada.AsNoTracking().ToListAsync());
        }



    }
}