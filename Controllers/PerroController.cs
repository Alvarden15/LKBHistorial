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
    [Authorize]
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

        //Se genera los listados de los registros mediante la llave foranea
        
        public void ListadoPerros(){
            //Se hace un listado primero
            var perros= _context.Perro.AsNoTracking().ToList();
            //Luego se crea un ViewBag tipo SelectList, con el campo que leerá y en campo que mostrará
            ViewBag.ListaPerros= new SelectList(perros,"Id","Nombre");
        }
        public void ListadoEstatura2(){
            var estaturas= _context.TipoEstatura.AsNoTracking().Where(m=>m.Id!=4);
            ViewBag.Estaturas2=new SelectList(estaturas,"Id","Estatura");
        }

        public void ListadoEstatura(){
            var estaturas= _context.TipoEstatura.AsNoTracking().ToList();
            ViewBag.Estaturas=new SelectList(estaturas,"Id","Estatura");
        }

        public void ListadoCriaderos(){
            var criaderos=_context.Criadero.AsNoTracking().ToList();
            ViewBag.Criaderos=new SelectList(criaderos,"Id","Nombre");
        }

        public void LCriaderos(int idnom){
            var cria=_context.Criadero.AsNoTracking().FirstOrDefault(m=>m.Id==idnom);
            ViewBag.Cria=cria;
        }


        public void ListadoCriadores(){
            var criador=_context.Criador.AsNoTracking().ToList();
            ViewBag.Criadores=new SelectList(criador,"Id","Nombre");
        }

        public void ListadoRazas(){
            var razas=_context.RazaPerro.AsNoTracking().ToList();
            ViewBag.Razas= new SelectList(razas,"Id","Raza");
        }

        public void ListadoHembras(){
            var hembras=_context.Perro.AsNoTracking().Where(m=>m.Sexo.Contains("H") && m.Madurez.Contains("A"));
            ViewBag.Hembras=new SelectList(hembras,"Id","Nombre");
        }

        public void ListadoMachos(){
            var machos=_context.Perro.AsNoTracking().Where(o=>o.Sexo.Contains("M") && o.Madurez.Contains("A"));
            ViewBag.Machos=new SelectList(machos,"Id","Nombre");
        }

        /*Las paginas en si (que no son de listado) */

        public IActionResult RegistrarPerro(){
            
            ListadoCriaderos();
            ListadoCriadores();
            ListadoRazas();
            ListadoEstatura2();
            ListadoHembras();
            ListadoMachos();
            ListadoPerros();
            return View();
        }
        public IActionResult BorrarPerro(){
            return View();
        }

        public IActionResult RegistrarCriador(){
            ListadoCriaderos();
            return View();
        }

        public IActionResult RegistrarCriadero(){
            return View();
        }


        public IActionResult ConfirmacionPerros(){
            return RedirectToAction("ListaPerros","Perro");
        }
        public IActionResult ConfirmacionNormal(){
            return RedirectToAction("Index","Home");
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
        public async Task<IActionResult> RegistrarPerro([Bind("Id,IdRaza,Sexo,FechaNacimiento,Nombre,Madurez,Temperamento,IdEstatura,IdCriadorActual,IdCriadorOriginal,IdCriadero,IdPadre,IdMadre")]Perro perro){
            /* El ModelState.IsValid verifica que los datos que se registran o modifican cumplan
            con los requisitos que se definieron en sus respectivos modelos */
                if(ModelState.IsValid){
                    
                    
                    if(perro.Madurez.Equals("C")){
                        perro.IdCriadero=4;
                    }
                    
                    //perro.IdCriadero=SeleccionarCriadero(perro.IdCriadorActual);
                   _context.Add(perro);
                   // Con el await, el comando se carga de forma paralela sin necesidad de bloquear ninguna otra función
                   await _context.SaveChangesAsync();
                    
                   // El RedirectToAction te envia a una pagina en especifico una vez hecha la operación
                   // Los parametros son "El nombre de la pagina","El nombre del controlador a la que esta unida (si esta en otro)"

                   return RedirectToAction("ConfirmacionPerros");
                }
            
            ListadoCriaderos();
            ListadoCriadores();
            ListadoRazas();
            ListadoPerros();
            ListadoEstatura2();
            ListadoHembras();
            ListadoMachos();
            return View(perro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarCriador([Bind("Id,Nombre,ApellidoPaterno,ApellidoMaterno,Telefono,IdCriadero")]Criador criador){
            if(ModelState.IsValid){
                _context.Criador.Add(criador);
                await _context.SaveChangesAsync();
                return RedirectToAction("ConfirmacionNormal");
            }
            ListadoCriaderos();
            return View(criador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarCriadero([Bind("Id,Nombre,Departamento,Distrito,Calle")]Criadero criadero){
            if(ModelState.IsValid){
                _context.Criadero.Add(criadero);
                await _context.SaveChangesAsync();
                return RedirectToAction("ConfirmacionNormal");
            }
            return View(criadero);
        }

        /*Desde aquí estan los listados */

        [AllowAnonymous]
        public async Task<IActionResult> ListaPerros(String nombre, int? raza){

            var perros= from m in _context.Perro select m;
            if(!String.IsNullOrEmpty(nombre) || raza!=null){
                
                ListadoRazas();
                // Recuerden, con entity framework se usa linq para las consultas de base de datos, asi que hay que ser creativos
                perros= perros.AsNoTracking().Where(m=>m.Nombre.Equals(nombre,StringComparison.OrdinalIgnoreCase) ||m.IdRaza==raza ).OrderByDescending(d=>d.FechaNacimiento);
               
            }
           
            ListadoRazas();
            return View(await perros.AsNoTracking().OrderByDescending(d=>d.FechaNacimiento).Include(m=>m.Criadero).ThenInclude(d=>d.Perro).ThenInclude(r=>r.RazaPerro).ToListAsync());

        }

        public async Task<IActionResult> ListaCriaderos(){

            
            return View(await _context.Criadero.ToListAsync());
        }

         public async Task<IActionResult> ListarCriaderos(){
            return View(await _context.Criadero.ToListAsync());
        }
        
        /*Desde aqui estan las operaciones para entrar a una pagina al detectar una id (u otro campo) */

        //Para las pagina de eliminar

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
        [AllowAnonymous]
        public async Task<IActionResult> Arbol(int? id){
            //Se verifica si esta presente en la tabla
            if(id==null){
                return NotFound();
            }
            
            //El include permite ver las propiedades de una llave foranea
            //El ThenInclude continua obteniendo otros atributos desde el mismo include
            //Se puede añadir multiples includes
            var perros= await _context.Perro.AsNoTracking()
            .Include(m=>m.Padre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                .ThenInclude(m=>m.Padre).ThenInclude(m=>m.Criadero)

            .Include(m=>m.Padre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                .ThenInclude(m=>m.Madre).ThenInclude(m=>m.Criadero)

           
            .Include(d=>d.Madre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                .ThenInclude(m=>m.Padre).ThenInclude(m=>m.Criadero)

           
            .Include(d=>d.Madre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                .ThenInclude(m=>m.Madre).ThenInclude(m=>m.Criadero)

                    
            .Include(m=>m.Criadero)
            .SingleOrDefaultAsync(m=>m.Id==(id));
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
            ListadoCriaderos();
            ListadoCriadores();
            ListadoRazas();
            ListadoPerros();
            ListadoEstatura();
            ListadoHembras();
            ListadoMachos();

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
            return RedirectToAction("ConfirmacionPerros");
        }



        //Modificacion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarPerro(int? id, [Bind("Id,IdRaza,Sexo,FechaNacimiento,Nombre,Madurez,Temperamento,IdEstatura,IdCriadorActual,IdCriadorOriginal,IdCriadero,IdPadre,IdMadre")]Perro perro){
            if(id!=perro.Id){
                return NotFound();
            }

            if(ModelState.IsValid){

                //El try-catch es por si existe algún fallo en la base de datos o por si ya se ha modificado algun campo por otro
               try{
                if(perro.Madurez.Equals("C")){
                    perro.IdCriadero=4;
                }
                _context.Perro.Update(perro);
                await _context.SaveChangesAsync();
               }catch(DbUpdateConcurrencyException){
                if(!VerificarPerro(perro.Id)){
                    return NotFound();
                }else{
                    throw;
                }
               }
                 return RedirectToAction("ConfirmacionPerros");

            }
            ListadoCriaderos();
            ListadoCriadores();
            ListadoRazas();
            ListadoPerros();
            ListadoEstatura();
            ListadoHembras();
            ListadoMachos();
            return View(perro);
           
        }


        // Se ve si es que la entidad con el campo asignado se encuentra en la base de datos
        bool VerificarPerro(int id){
            return _context.Perro.Any(m=>m.Id==id);
        }

        bool VerificarCriador(int id){
            return _context.Criador.Any(m=>m.Id==id);
        }
         bool VerificarCriadero(int id){
            return _context.Criadero.Any(m=>m.Id==id);
        }


        //Comando que puede que esten o no en el entregable

        /* El arbol hasta los bisabuelos

              public async Task<IActionResult> Arbol(int? id){
            //Se verifica si esta presente en la tabla
            if(id==null){
                return NotFound();
            }
            
            //El include permite ver las propiedades de una llave foranea
            //El ThenInclude continua obteniendo otros atributos desde el mismo include
            //Se puede añadir multiples includes
            var perros= await _context.Perro.AsNoTracking()
            .Include(m=>m.Padre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                .ThenInclude(m=>m.Padre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                    .ThenInclude(m=>m.Padre).ThenInclude(m=>m.Criadero)

            .Include(m=>m.Padre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                .ThenInclude(m=>m.Padre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                    .ThenInclude(m=>m.Madre).ThenInclude(m=>m.Criadero)

            .Include(m=>m.Padre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                .ThenInclude(m=>m.Madre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                    .ThenInclude(m=>m.Padre).ThenInclude(m=>m.Criadero)

            .Include(m=>m.Padre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                .ThenInclude(m=>m.Madre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                    .ThenInclude(m=>m.Madre).ThenInclude(m=>m.Criadero)

            .Include(d=>d.Madre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                .ThenInclude(m=>m.Padre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                    .ThenInclude(m=>m.Padre).ThenInclude(m=>m.Criadero)

            .Include(d=>d.Madre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                .ThenInclude(m=>m.Padre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                    .ThenInclude(m=>m.Madre).ThenInclude(m=>m.Criadero)

            .Include(d=>d.Madre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                .ThenInclude(m=>m.Madre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                    .ThenInclude(m=>m.Padre).ThenInclude(m=>m.Criadero)

            .Include(d=>d.Madre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                .ThenInclude(m=>m.Madre).ThenInclude(m=>m.Criadero).ThenInclude(m=>m.Perro)
                    .ThenInclude(m=>m.Madre).ThenInclude(m=>m.Criadero)
                    
            .Include(m=>m.Criadero)
            .SingleOrDefaultAsync(m=>m.Id==(id));
            if(perros==null){
                return NotFound();
            }

            return View(perros);
        }

        
        
        
         */


         /*
        public void ListadoCriaderoNormal(int id){
            var criaderos=_context.Criadero.AsNoTracking().Where(c=>c.Id==id);
            ViewBag.CriaderoCriador=new SelectList(criaderos,"Id","Nombre");

        }
         */

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