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

namespace LKBHistorial.Controllers{
    public class PersonaController:Controller{
        
        private readonly MvcContext _context;

        public PersonaController(MvcContext cont){
            _context=cont;
        }

        public IActionResult RegistarPersona(){

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistarPersona([Bind("Id,Nombre,ApellidoPaterno,ApellidoMaterno")]Persona persona){
            if(ModelState.IsValid){
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction("ConfirmacionPersona");

            }

            return View(persona);
        }

        public IActionResult ConfirmacionPersona(){

            return RedirectToAction("Index","Home");
        }



    }

}