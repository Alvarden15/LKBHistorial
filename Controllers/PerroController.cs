using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LKBHistorial.Models;
using Microsoft.EntityFrameworkCore;
using Models.MvcContext;



namespace LKBHistorial.Controllers
{
    public class PerroController:Controller
    {
        private readonly MvcContext _context;

        public PerroController(MvcContext context){
            _context=context;
        }


        public IActionResult RegistrarPerro(){

            return View();
        }

        public IActionResult BorrarPerro(){
            return View();
        }

        public async Task<IActionResult> ArbolGeneologico(){


            return View(await _context.Perro.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarPerro([Bind("NombrePerro,Edad")]Perro perro){

                if(ModelState.IsValid){
                   _context.Add(perro);
                   await _context.SaveChangesAsync();
                   return RedirectToAction("Index","Home");
                }

            return View(perro);
        }
        
        public async Task<IActionResult> EliminarPerro(int? id){
            if(id==null){
                return NotFound();
            }
            var perros= await _context.Perro.SingleOrDefaultAsync(m=>m.IDPerro==id);

            if(perros==null){
                return NotFound();
            }

            return View(perros);
        }


        public async Task<IActionResult> ModificarPerro(int? id){
            if(id==null){
                return NotFound();
            }

            var perro=await _context.Perro.SingleOrDefaultAsync(m=>m.IDPerro==id);
            if(perro==null){
                return NotFound();
            }

            return View(perro);
        }

        [HttpPost,ActionName("EliminarPerro")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEliminacion(int id){
            var perro= await _context.Perro.SingleOrDefaultAsync(m=>m.IDPerro==id);
            _context.Perro.Remove(perro);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarPerro(int? id, [Bind("IDPerro,NombrePerro,Edad")]Perro perro){
            if(id!=perro.IDPerro){
                return NotFound();
            }

            if(ModelState.IsValid){
               try{
                _context.Perro.Update(perro);
                await _context.SaveChangesAsync();
               }catch(DbUpdateConcurrencyException){
                if(!VerificarPerro(perro.IDPerro)){
                    return NotFound();
                }else{
                    throw;
                }
               }
                 return RedirectToAction("ArbolGeneologico","Perro");

            }
            return View(perro);

           
        }


        bool VerificarPerro(int id){
            return _context.Perro.Any(m=>m.IDPerro==id);
        }

    }
}