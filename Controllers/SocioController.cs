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
           
            var socios=_context.Socio.AsNoTracking().Select(so=>so.Id).ToArray();
            var deudores=_context.Deudor.AsNoTracking().Select(d=>d.Id).ToArray();
            //var personas =_context.Persona.AsNoTracking().ToList();
            //var per=_context.Persona.Except(socios).ToList();
            var personas=_context.Persona.AsNoTracking().Where(per=>!socios.Contains(per.Id) && !deudores.Contains(per.Id)).ToList();
            ViewBag.Personas= new SelectList(personas,"Id","Nombre");
        }

        public void ListadoSocios(){
            
            var so=_context.Socio.AsNoTracking().Select(o=>o.Id).ToArray();
            var personas=_context.Persona.AsNoTracking().Where(p=>so.Contains(p.Id)).ToList();

            //var socios= _context.Socio.AsNoTracking().Include(s=>s.PersonaSocio).ToList();

            ViewBag.Socios= new SelectList(personas,"Id","Nombre");
        }

        public void ListadoPersonas2(){
            var deudores=_context.Deudor.AsNoTracking().Select(d=>d.Id).ToArray();
            var personas= _context.Persona.AsNoTracking().Where(p=>!deudores.Contains(p.Id)).ToList();
            ViewBag.ListPersona=new SelectList(personas,"Id","Nombre");
        }

        public IActionResult RegistrarSocio(){
            ListadoPersonas();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarSocio([Bind("Id,Departamento,Distrito,Calle,Pais")]Socio socio){
            var ids=VerificarSocio(socio.Id);
            if(ModelState.IsValid && !ids){
                _context.Add(socio);
                await _context.SaveChangesAsync();
                return RedirectToAction("ConfirmacionSocio");
            }
            if(ids){
                ModelState.AddModelError(String.Empty,"Ya esta registrado como Socio. Intenta con otra persona");
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
            ListadoPersonas2();
            return View(socio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarSocio(int? id,[Bind("Id,Departamento,Distrito,Calle,Pais")] Socio socio){
            if(id==null){
                return NotFound();
            }
            var sId=VerificarSocio(socio.Id);
            if(ModelState.IsValid){
                try{
                    _context.Socio.Update(socio);
                    await _context.SaveChangesAsync();
                    
                }catch(DbUpdateConcurrencyException){
                    if(!VerificarSocio(id)){
                        return NotFound();
                    }else{
                        throw;
                    }
                }
                return RedirectToAction("ConfirmacionSocio");
            }
            
             ListadoPersonas2();
            return View(socio);
        }


        public async Task<IActionResult> ListaSocios(){
            var socio= from m in _context.Socio select m;
            return View(await socio.AsNoTracking().Include(s=>s.PersonaSocio).ToListAsync());
        }

        public async Task<IActionResult> DetallesSocio(int? id){
            if(id==null){
                return NotFound();
            }
            var socio=await _context.Socio.AsNoTracking().Include(p=>p.PersonaSocio).SingleOrDefaultAsync(s=>s.Id==id);
            if(socio==null){
                return NotFound();
            }

            return View(socio);
        }

        public IActionResult ConfirmacionSocio(){
            return RedirectToAction("ListaSocios");
        }

        public IActionResult AsignarPerroSocio(){
            ListadoSocios();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarPerroSocio([Bind("Nombre,Sexo,IdSocio")]PerroSocio perrosocio){
            if(ModelState.IsValid){
                _context.Add(perrosocio);
                await _context.SaveChangesAsync();
                return RedirectToAction("ConfirmacionSocio");
            }
            ListadoSocios();
            return View(perrosocio);
        }

        public async Task<IActionResult> Asignaciones(){
            var asignaciones= from m in _context.PerroSocio select m;

            return View(await asignaciones.AsNoTracking().Include(p=>p.SocioNavigation).ThenInclude(d=>d.PersonaSocio).ToListAsync());
        }

        public async Task<IActionResult> EliminarSocio(int? id){
            if(id==null){
                return NotFound();
            }

            var socio= await _context.Socio.SingleOrDefaultAsync(s=>s.Id==id);
            if(socio==null){
                return NotFound();
            }

            return View(socio);
        }

        [HttpPost,ActionName("EliminarSocio")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEliminacionSocio(int? id){
            var socio= await _context.Socio.SingleOrDefaultAsync(s=>s.Id==id);
            _context.Remove(socio);
            await _context.SaveChangesAsync();

            return RedirectToAction("ListaSocios");
        }

        
        public bool VerificarSocio(int? id){
            return _context.Socio.Any(s=>s.Id==id);
        }

        public bool VerificarDeudor(int? id){
            return _context.Deudor.Any(d=>d.Id==id);
        }


        

    }



}