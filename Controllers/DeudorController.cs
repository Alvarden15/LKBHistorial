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

        public void ListadoPersonas(){
           
            var socios=_context.Socio.AsNoTracking().Select(so=>so.Id).ToArray();
            var deudores=_context.Deudor.AsNoTracking().Select(d=>d.Id).ToArray();
            //var personas =_context.Persona.AsNoTracking().ToList();
            //var per=_context.Persona.Except(socios).ToList();
            var personas=_context.Persona.AsNoTracking().Where(per=>!socios.Contains(per.Id) && !deudores.Contains(per.Id)).ToList();
            ViewBag.Personas= new SelectList(personas,"Id","Nombre");
        }

        public void ListadoPersonas2(){
            //Este es el listado de personas que no estan en la tabla de socios
            var socios=_context.Socio.AsNoTracking().Select(so=>so.Id).ToArray();
            var personas= _context.Persona.AsNoTracking().Where(p=>!socios.Contains(p.Id)).ToList();
            ViewBag.ListPersona=new SelectList(personas,"Id","Nombre");
        }

        public void ListadoDeudores(){
            //Este es el listado de deudores propiamente dicha
            var de=_context.Deudor.AsNoTracking().Select(d=>d.Id).ToArray();
            var personas=_context.Persona.AsNoTracking().Where(p=>de.Contains(p.Id)).ToList();

            //var deudores= _context.Deudor.AsNoTracking().Include(m=>m.PersonaDeudor).ToList();
            ViewBag.Deudores= new SelectList(personas,"Id","Nombre");
        }




        public IActionResult RegistrarDeudor(){
            ListadoPersonas();
            return View();
        }

        public IActionResult RegistrarDeuda(){
           
            ListadoDeudores();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarDeudor([Bind("Id,Correo,Estado")]Deudor deudor){
            if(ModelState.IsValid){
                _context.Add(deudor);
                await _context.SaveChangesAsync();
                return RedirectToAction("ConfirmacionDeudor","Deudor");
            }
            ListadoPersonas();
            return View(deudor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarDeuda([Bind("IdDeudor,Cuotas,MontoInicial,MontoTotal,NumeroCuotas,SaldoPendiente,CuotasPendientes,FechaInicio")]Deuda deuda){
              if(ModelState.IsValid){
                _context.Add(deuda);
                await _context.SaveChangesAsync();
                return RedirectToAction("ConfirmacionDeudor","Deudor");
            }
            ListadoDeudores();
            return View(deuda);
        }





        public async Task<IActionResult> ListaDeudores(){
            var deudor= from m in _context.Deudor select m;
        
            return View(await deudor.AsNoTracking().Include(d=>d.PersonaDeudor).ToListAsync());
        }

        public async Task<IActionResult> ListaDeudas(int? id){
            var deudas= from m in _context.Deuda select m;
            if(id!=null){
                ListadoDeudores();
                deudas=deudas.AsNoTracking().Where(o=>o.IdDeudor==id);
            }
            ListadoDeudores();
            return View(await deudas.AsNoTracking().Include(d=>d.DeudorNavigation).ThenInclude(s=>s.PersonaDeudor).ToListAsync());
        }

        public IActionResult Estadisticas(){

            return View();
        }

        public async Task<IActionResult> DetallesDeudor(int? id){
            if(id==null){
                return NotFound();
            }
            var deudor= await _context.Deudor.SingleOrDefaultAsync(d=>d.Id==id);
            if(deudor==null){
                return NotFound();
            }

            return View(deudor);
        }

        public async Task<IActionResult> DetallesDeuda(int? id){
            if(id==null){
                return NotFound();
            }
            var deuda= await _context.Deuda.SingleOrDefaultAsync(d=>d.Id==id);
            if(deuda==null){
                return NotFound();
            }

            return View(deuda);
        }

       
        public async Task<IActionResult> ModificarDeudor(int? id){
            if(id==null){
                return NotFound();
            }

            var deudor= await _context.Deudor.SingleOrDefaultAsync(s=>s.Id==id);
            if(deudor==null){
                return NotFound();
            }
            ListadoPersonas2();

            return View(deudor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarDeudor(int? id, Deudor deudor){
            
            if(id==null){
                return NotFound();
            }
            var d= await _context.Persona.ToListAsync();
            if(ModelState.IsValid ){
                try{
                    _context.Deudor.Update(deudor);
                    await _context.SaveChangesAsync();
                    
                }catch(DbUpdateConcurrencyException){
                    if(!VerificarDeudor(id)){
                        return NotFound();
                    }else{
                        throw;
                    }
                }
                return RedirectToAction("ConfirmacionDeudor");

            }
            ListadoPersonas2();

            return View(deudor);
        }
        public async Task<IActionResult> EliminarDeudor(int? id){
            if(id==null){
                return NotFound();
            }

            var deudor= await _context.Deudor.SingleOrDefaultAsync(s=>s.Id==id);
            if(deudor==null){
                return NotFound();
            }

            return View(deudor);
        }

        [HttpPost,ActionName("EliminarDeudor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmacionEliminacionDeudor(int? id){
            var deuda=await _context.Deuda.SingleOrDefaultAsync(d=>d.Id==id);
            _context.Remove(deuda);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListaDeudores");
        }



        public IActionResult ConfirmacionDeudor(){

            return RedirectToAction("ListaDeudores");
        }

        public bool VerificarDeudor(int? id){
            return _context.Deudor.Any(d=>d.Id==id);
        }



        
        
    }
}