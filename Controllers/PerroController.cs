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
    public class PerroController:Controller
    {
        //private IMemoryCache cache;
        //private String filelocation="";

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

        //Se genera la lista de perros mediante la llave foranea
        public void ListarPerros(){
            var perros= _context.Perro.ToList();
            ViewBag.ListPerros= new SelectList(perros,"Id","Nombre");
        }

        public void ListadoCriaderos(){
            var criaderos=_context.Criadero.ToList();
            ViewBag.Criaderos=new SelectList(criaderos,"Id","Nombre");
        }

        public void ListadoCriadores(){
            var criador=_context.Criadero.ToList();
            ViewBag.Criadores=new SelectList(criador,"Id","Nombre");
        }

        public void ListadoRazas(){
            var razas=_context.RazaPerro.ToList();
            ViewBag.Razas= new SelectList(razas,"Id","Tipo");
        }

        /*Las paginas en si (que no son de listado) */

        public IActionResult RegistrarPerro(){
            ListadoCriaderos();
            ListadoCriadores();
            ListadoRazas();
            return View();
        }
        public IActionResult RegistrarLunada(){
            return View();
        }

        public IActionResult RegistrarPrenada(){
            return View();
        }
        public IActionResult BorrarPerro(){
            return View();
        }

        public IActionResult RegistrarCriador(){
            return View();
        }

        public IActionResult RegistrarCriadero(){
            return View();
        }

        /*
        public async Task<IActionResult> ListaHembras(){
            return View(await _context.Reproductora.ToListAsync());
        }
         */
        
        /*Desde aqui estarán los registros */
        
        //El HttpPost asegura que realice una petición post
        //El ValidateAntiForgeryToken permite aplicar la validación de modelos
        [HttpPost]
        [ValidateAntiForgeryToken]

        // Con el Bind se asegura que al momento de registrar se toma en cuenta los campos asignados (util si ninguna admite vacios)
        /* Con el async se asegura que la función que se ejecuta se haga de forma asyncrona;
         es decir, sin retrasos producidos por la base de datos*/      
        public async Task<IActionResult> RegistrarPerro([Bind("Id,Nombre,IdRazaPerro,IdCriadero,IdDueno,DuenoActual,Sexo,FechaNacimiento,Padre,Madre")]Perro perro){
            
            /* El ModelState.IsValid verifica que los datos que se registran o modifican cumplan
            con los requisitos que se definieron en sus respectivos modelos */
                if(ModelState.IsValid){
                   _context.Add(perro);

                   // Con el await, el comando se carga de forma paralela sin necesidad de bloquear ninguna otra función
                   await _context.SaveChangesAsync();

                   // El RedirectToAction te envia a una pagina en especifico una vez hecha la operación
                   // Los parametros son "El nombre de la pagina","El nombre del controlador a la que esta unida (si esta en otro)"
                   return RedirectToAction("Index","Home");
                }
            ListadoCriaderos();
            ListadoCriadores();
            ListadoRazas();
            return View(perro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarCriador([Bind("Id,Nombre,ApellidoPaterno,ApellidoMaterno,Telefono,IdCriadero")]Criador criador){
            if(ModelState.IsValid){
                _context.Criador.Add(criador);
                await _context.SaveChangesAsync();
                return RedirectToAction("");
            }
            return View(criador);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarPrenada([Bind("Id,IdMonta,CantidadInseminada,FechaInicio,FechaFin,IdParto,FechaCesaria,NumeroCamadas")]Prenada prenada){
            if(ModelState.IsValid){
                _context.Prenada.Add(prenada);
                await _context.SaveChangesAsync();
                return RedirectToAction("");
            }
            return View(prenada);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarLunada([Bind("Id,FechaInicio,FechaFin,NumeroCelos")]Lunada lunada){
            if(ModelState.IsValid){
                _context.Lunada.Add(lunada);
                await _context.SaveChangesAsync();
                return RedirectToAction("");
            }
            return View(lunada);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarCriadero([Bind("Id,Nombre,Departamento,Distrito,Calle")]Criadero criadero){
            if(ModelState.IsValid){
                _context.Criadero.Add(criadero);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            return View(criadero);
        }

        /*Desde aquí estan los listados */

        public async Task<IActionResult> ListaPerros(String nombre, int? raza){
            var perros= from m in _context.Perro select m;
            if(!String.IsNullOrEmpty(nombre) || raza!=null){

                // Recuerden, con entity framework se usa linq para las consultas de base de datos, asi que hay que ser creativos
                perros= perros.Where(m=>m.Nombre.Contains(nombre) ||m.IdRaza==raza );
            }
            return View(await _context.Perro.ToListAsync());
        }

        public async Task<IActionResult> ListarCriadero(){

            
            return View(await _context.Criadero.ToListAsync());
        }

        public async Task<IActionResult> ListarPrenadas(){
            return View(await _context.Prenada.ToListAsync());
        }

        
        public async Task<IActionResult> ListarLunadas(){
            return View(await _context.Lunada.ToListAsync());
        }

         public async Task<IActionResult> ListarCriaderos(){
            return View(await _context.Criadero.ToListAsync());
        }
        
        /*Desde aqui estan las operaciones para entrar a una pagina al detectar una id (u otro campo) */

        //Para las pagina de eliminar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarPerro(int? id){

            // Se verifica si una entidad esta registrada en la base de datos o no
            if(id==null){
                return NotFound();
            }
            var perros= await _context.Perro.SingleOrDefaultAsync(m=>m.Id==id);

            if(perros==null){
                return NotFound();
            }

            return View(perros);
        }
        
        //Para las paginas de los detalles
        public async Task<IActionResult> DetallesPerro(int? id){
            if(id==null){
                return NotFound();
            }
            //Se verifica si esta presente en la tabla
            var perros= await _context.Perro.SingleOrDefaultAsync(m=>m.Id==(id));
            if(perros==null){
                return NotFound();
            }

            return View(perros);
        }

        //Para las paginas de modificaciones
        public async Task<IActionResult> ModificarPerro(int? id){

            // Antes de entrar a la pagina de modificación se valida si esta en la tabla o no
            if(id==null){
                return NotFound();
            }

            var perro=await _context.Perro.SingleOrDefaultAsync(m=>m.Id==id);
            if(perro==null){
                return NotFound();
            }

            return View(perro);
        }

        /*Las operaciones en si */

        //Eliminacion
        // En esta ocación se llama aqui al metodo que tiene los parametros
        [HttpPost,ActionName("EliminarPerro")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEliminacion(int id){
            var perro= await _context.Perro.SingleOrDefaultAsync(m=>m.Id==id);
            _context.Perro.Remove(perro);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }

        //Modificacion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarPerro(int? id, [Bind("IDPerro,NombrePerro,Edad")]Perro perro){
            if(id!=perro.Id){
                return NotFound();
            }

            if(ModelState.IsValid){

                //El try-catch es por si existe algún fallo en la base de datos o por si ya se ha modificado algun campo por otro
               try{
                _context.Perro.Update(perro);
                await _context.SaveChangesAsync();
               }catch(DbUpdateConcurrencyException){
                if(!VerificarPerro(perro.Id)){
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
        bool VerificarPerro(int id){
            return _context.Perro.Any(m=>m.Id==id);
        }

        //Comando que puede que esten o no en el entregable

         /*
         [HttpGet]
        public async Task<IActionResult> ArbolGeneologico(int? id,String nombre, String tipo){
            // Se verifica que todo este en orden en la base de datos
            var perros= from m in _context.Perro select m;
            if(!String.IsNullOrEmpty(nombre) || !String.IsNullOrEmpty(tipo)){

                // Recuerden, con entity framework se usa linq para las consultas de base de datos, asi que hay que ser creativos
                perros= perros.Where(m=>m.Id==id 
               );
            }
           return View(await _context.Perro.ToListAsync());
        }
         */

          /*
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
         */ 

        // Transforma los bytes almacenados en la base de datos en un link de la imagen
        /*
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
        
         */
        

    }
}