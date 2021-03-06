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
        //Se listan los perros que aún no esten asignados
        public void ListadoPerros(){
            
            var perrosocios=_context.PerroSocio.Select(s=>s.IdPerro).ToArray();
            var perros= _context.Perro.AsNoTracking().Where(p=>!perrosocios.Contains(p.Id)).ToList();
            ViewBag.PerrosAsociar=new SelectList(perros,"Id","Nombre");
        }

        //El listado de personas para ver si no estan como socios o deudores
        public void ListadoPersonas(){
            
            var socios=_context.Socio.AsNoTracking().Select(so=>so.Id).ToArray();
            var deudores=_context.Deudor.AsNoTracking().Select(d=>d.Id).ToArray();
            //var personas =_context.Persona.AsNoTracking().ToList();
            //var per=_context.Persona.Except(socios).ToList();
            var personas=_context.Persona.AsNoTracking().Where(per=>!socios.Contains(per.Id) && !deudores.Contains(per.Id)).ToList();
            ViewBag.Personas= new SelectList(personas,"Id","Nombre");
        }
        //El listado de socios para seleccionar
        public void ListadoSocios(){
            
            var so=_context.Socio.AsNoTracking().Select(o=>o.Id).ToArray();
            var personas=_context.Persona.AsNoTracking().Where(p=>so.Contains(p.Id)).ToList();

            //var socios= _context.Socio.AsNoTracking().Include(s=>s.PersonaSocio).ToList();

            ViewBag.Socios= new SelectList(personas,"Id","Nombre");
        }
        //Para listar las personas que no son deudores
        public void ListadoPersonas2(){
            var deudores=_context.Deudor.AsNoTracking().Select(d=>d.Id).ToArray();
            var persona= _context.Persona.AsNoTracking().Where(p=>!deudores.Contains(p.Id)).ToList();
            ViewBag.ListPersona=new SelectList(persona,"Id","Nombre");
        }

        //Se busca el socio de la id buscada
        public void EncontrarSocioAsignacion(int? i){
            var personas=_context.Persona.AsNoTracking().Where(d=>d.Id==i).ToList();
            ViewBag.Socios=new SelectList(personas,"Id","Nombre");
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
                return RedirectToAction("AsignarPerroSocio", new{id=socio.Id});
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

        public IActionResult AsignarPerroSocio(int? id){
            if(id!=null){
                EncontrarSocioAsignacion(id);
            }else{
                ListadoSocios();
            }
            ListadoPerros();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarPerroSocio([Bind("IdSocio,IdPerro")]PerroSocio perrosocio){
            var cantidad=_context.PerroSocio.Where(s=>s.IdSocio==perrosocio.IdSocio).ToList();
            var iperro=VerificarPerroAsociado(perrosocio.IdPerro);
            /*
             if(ModelState.IsValid)
            */ 
            
            if(ModelState.IsValid && cantidad.Count()<2 && !iperro){
                _context.Add(perrosocio);
                await _context.SaveChangesAsync();
                return RedirectToAction("ConfirmacionSocio");
            }
            if(cantidad.Count()>1){
                ModelState.AddModelError(string.Empty,"El socio o socia alcanzó el maximo de perros que puede tener. Por favor, asigna a otro");
                ListadoSocios();
            }else{
                EncontrarSocioAsignacion(perrosocio.IdSocio);
            }
            if(iperro){
                 ModelState.AddModelError(string.Empty,"El perro ya fue asociado por otro socio. Elige otro perro");
            }
            ListadoPerros();
            return View(perrosocio);
        }

        public async Task<IActionResult> Asignaciones(int? id){
            var asignaciones= from m in _context.PerroSocio select m;
            if(id!=null){
                asignaciones=asignaciones.AsNoTracking().Where(a=>a.IdSocio==id);
            }

            return View(await asignaciones.AsNoTracking().Include(p=>p.SocioNavigation).ThenInclude(d=>d.PersonaSocio).Include(w=>w.PerroAsociado).ToListAsync());
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

        public bool VerificarPerroAsociado(int? id){
            return _context.PerroSocio.Any(d=>d.IdPerro==id);
        }


        

    }



}