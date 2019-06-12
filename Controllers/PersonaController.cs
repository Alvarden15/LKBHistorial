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

        public IActionResult RegistrarPersona(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarPersona([Bind("Id,Nombre,ApellidoPaterno,ApellidoMaterno")]Persona persona){
            var digitos=persona.Id.ToString().Length;
            /*
            
            if(digitos>7){

            }
            if(digitos<=7){
                ModelState.AddModelError(string.Empty,"El id debe de tener como minimo 7 digitos. Recomendamos ingresar el DNI o pasaporte de la persona");
            }
             */
            
            var idPersona=verificarPersona(persona.Id);
            if(ModelState.IsValid && !idPersona && digitos>7){
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction("ConfirmacionPersona");

            }
            if(idPersona){
                ModelState.AddModelError(string.Empty,"El ID ya existe");
            }
            if(digitos<=7){
                ModelState.AddModelError(string.Empty,"El ID debe de tener como minimo 8 digitos. Recomendamos ingresar el DNI o pasaporte de la persona");
            }

            return View(persona);
        }

        public IActionResult ConfirmacionPersona(){

            return RedirectToAction("Index","Home");
        }

        public bool verificarPersona(int id){
            return _context.Persona.Any(p=>p.Id==id);
        }



    }

}