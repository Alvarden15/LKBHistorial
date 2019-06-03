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

        public void ListadoDeudores(){
            var deudores= _context.Deudor.AsNoTracking().Include(m=>m.PersonaDeudor).ToList();
            ViewBag.Deudores= new SelectList(deudores,"Id","Nombre");
        }

        public IActionResult RegistrarDeudor(){
            ListadoPersonas();
            return View();
        }

        public IActionResult RegistrarDeuda(){
            ListadoPersonas();
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

            return View(await deudor.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> ListaDeudas(){
            var deudas= from m in _context.Deuda select m;

            return View(await deudas.AsNoTracking().ToListAsync());
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarDeudor(int? id){
            if(id==null){
                return NotFound();
            }

            var deudor= await _context.Deudor.SingleOrDefaultAsync(s=>s.Id==id);
            if(deudor==null){
                return NotFound();
            }

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




        
        
    }
}