using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using LKBHistorial.Models;
using Microsoft.EntityFrameworkCore;
using Models.MvcContext;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;


namespace LKBHistorial.Controllers
{
    public class PerroController:Controller
    {
        private IMemoryCache cache;
        private String filelocation="";

        /* 
         Esto es por si queremos agregar memoria cache:

         public PerroController(MvcContext context, IMemoryCache memory){
            _context=context;
            cache=memory;
        }
        
        */

        private readonly MvcContext _context;

        public PerroController(MvcContext context){
            _context=context;
        }


        public IActionResult RegistrarPerro(){

            return View();
        }
        public IActionResult RegistrarCelada(){
            return View();
        }

        public IActionResult RegistrarPrenada(){
            return View();
        }

        public IActionResult RegistrarReproductora(){
            return View();
        }
        public IActionResult BorrarPerro(){
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ArbolGeneologico(String nombre, String tipo){

            // Se verifica que todo este en orden en la base de datos
            var perros= from m in _context.Perro select m;
            if(!String.IsNullOrEmpty(nombre) || !String.IsNullOrEmpty(tipo)){

                // Recuerden, con entity framework se usa linq para las consultas de base de datos, asi que hay que ser creativos
                perros= perros.Where(m=>m.NombrePerro.Contains(nombre,StringComparison.OrdinalIgnoreCase) 
                || m.TipoPerro.Contains(tipo));
            }
            return View(await _context.Perro.ToListAsync());
        }

        public async Task<IActionResult> ListaHembras(){
            return View(await _context.Reproductora.ToListAsync());
        }
        
        //El HttpPost se cambia el estado de la pagina que tiene el mismo nombre que la función
        [HttpPost]
        [ValidateAntiForgeryToken]

        // Con el Bind se asegura que al momento de registrar se toma en cuenta los campos asignados (util si ninguna admite vacios)
        /* Con el async se asegura que la función que se ejecuta se haga de forma asyncrona;
         es decir, sin retrasos producidos por la base de datos*/      
        public async Task<IActionResult> RegistrarPerro([Bind("IDPerro,NombrePerro,Edad")]Perro perro, IFormFile imag){
            
            var verificar=registrarImagen(imag);
            
            /* El ModelState.IsValid verifica que los datos que se registran o modifican cumplan
            con los requisitos que se definieron en sus respectivos modelos */
                if(ModelState.IsValid){
                    perro.Imagen=filelocation;
                    //if(!_context.Perro.Any(m=>m.IDPerro==perro.IDPerro || m.NombrePerro==perro.NombrePerro));
                   _context.Add(perro);

                   // Con el await, el comando se carga de forma paralela sin necesidad de bloquear ninguna otra función
                   await _context.SaveChangesAsync();

                   // El RedirectToAction te envia a una pagina en especifico una vez hecha la operación
                   // Los parametros son "El nombre de la pagina","El nombre del controlador a la que esta unida"
                   return RedirectToAction("Index","Home");
                }

            return View(perro);
        }
        
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarReproductora([Bind("IDReproductora,IDPerro, Fecha")]Reproductora Reproductora){
            if(ModelState.IsValid){
                _context.Reproductora.Add(Reproductora);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarPerro(String id){

            // Se verifica si una entidad esta registrada en la base de datos o no
            if(id==null){
                return NotFound();
            }
            var perros= await _context.Perro.SingleOrDefaultAsync(m=>m.IDPerro==id);

            if(perros==null){
                return NotFound();
            }

            return View(perros);
        }

        public async Task<IActionResult> DetallesPerro(String id){
            if(id==null){
                return NotFound();
            }
            var perros= await _context.Perro.SingleOrDefaultAsync(m=>m.IDPerro.Contains(id));
            if(perros==null){
                return NotFound();
            }

            return View(perros);
        }


        public async Task<IActionResult> ModificarPerro(String id){

            // Antes de entrar a la pagina de modificación se valida si esta en la tabla o no
            if(id==null){
                return NotFound();
            }

            var perro=await _context.Perro.SingleOrDefaultAsync(m=>m.IDPerro==id);
            if(perro==null){
                return NotFound();
            }

            return View(perro);
        }
        // En esta ocación se llama aqui al metodo que tiene los parametros
        [HttpPost,ActionName("EliminarPerro")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEliminacion(String id){
            var perro= await _context.Perro.SingleOrDefaultAsync(m=>m.IDPerro==id);
            _context.Perro.Remove(perro);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarPerro(String id, [Bind("IDPerro,NombrePerro,Edad")]Perro perro){
            if(id!=perro.IDPerro){
                return NotFound();
            }

            if(ModelState.IsValid){

                //El try-catch es por si existe algún fallo en la base de datos o por si ya se ha modificado algun campo por otro
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


        // Se ve si es que la entidad con el campo asignado se encuentra en la base de datos
        bool VerificarPerro(String id){
            return _context.Perro.Any(m=>m.IDPerro==id);
        }
        

        // Transforma los bytes almacenados en la base de datos en un link de la imagen
        public async Task<bool> registrarImagen(IFormFile file){
            
            if(file!=null && file.Length>0){
                try{
                     var name=Path.GetFileName(file.FileName);
                     var path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\images",name);
                     filelocation=path;
                    using (var filestream=new FileStream(path, FileMode.Create)){
                        await file.CopyToAsync(filestream);
                    }
                }catch(Exception e){
                    ViewBag.ErrorImagen="Error: "+e.Message.ToString();
                }
               
                return true;

            }
            return false;
        }

    }
}