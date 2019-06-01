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
    [Authorize("LKB Historial")]
    public class SocioController:Controller{
        private readonly MvcContext _context;

        public SocioController(MvcContext cont){
            _context=cont;
        }

        public void ListadoPerros(){
            var perros= _context.Perro.AsNoTracking().ToList();
            ViewBag.Perros=new SelectList(perros,"Id","Nombre");
        }

        public void ListadoPersonas(){
            var personas =_context.Perro.AsNoTracking().ToList();
            ViewBag.Personas= new SelectList(personas,"Id","Nombre");
        }

        public IActionResult RegistrarSocio(){
            ListadoPersonas();
            return View();
        }


        public async Task<IActionResult> RegistrarSocio(Socio socio){
            if(ModelState.IsValid){
                _context.Add(socio);
                await _context.SaveChangesAsync();
                return RedirectToAction("ConfirmacionSocio");
            }
            ListadoPersonas();
            return View(socio);
        }


        public async Task<IActionResult> ModificarSocio(int? id){
            if(id==null){
                return NotFound();
            }
            var socio= await _context.Socio.SingleOrDefaultAsync(m=>m.Id==id);
            if(socio==null){
                return NotFound();
            }
            ListadoPersonas();
            return View(socio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarSocio(int? id, Socio socio){
            if(id==null){
                return NotFound();
            }
            if(ModelState.IsValid){
                try{
                    _context.Socio.Update(socio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ConfirmacionSocio");
                }catch(DbUpdateConcurrencyException){
                    if(!VerificarSocio(id)){
                        return NotFound();
                    }else{
                        throw;
                    }
                }
            }
            ListadoPersonas();
            return View(socio);
        }


        public async Task<IActionResult> ListaSocios(){
            var socio= from m in _context.Socio select m;
            return View(await socio.AsNoTracking().ToListAsync());
        }

        public IActionResult ConfirmacionSocio(){
            return RedirectToAction("ListaSocios");
        }

        public IActionResult AsignarPerroSocio(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarPerroSocio(PerroSocio perrosocio){
            if(ModelState.IsValid){
                _context.Add(perrosocio);
                await _context.SaveChangesAsync();
                return RedirectToAction("ConfirmacionSocio");
            }
            return View(perrosocio);
        }

        public async Task<IActionResult> EliminarSocio(int? id){
            if(id==null){
                return NotFound();
            }

            var socio= await _context.Socio.SingleOrDefaultAsync(s=>s.Id==id);
            if(socio==null){
                return NotFound();
            }

            return View();
        }

        [HttpPost,ActionName("EliminarSocio")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEliminacion(int? id){
            var socio= await _context.Socio.SingleOrDefaultAsync(s=>s.Id==id);
            _context.Remove(socio);
            await _context.SaveChangesAsync();

            return RedirectToAction("ListaSocios");
        }

        
        public bool VerificarSocio(int? id){
            return _context.Socio.Any(s=>s.Id==id);
        }


        

    }



}