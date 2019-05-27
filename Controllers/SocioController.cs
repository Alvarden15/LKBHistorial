using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
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

namespace LKBHistorial.Controllers{
    public class SocioController:Controller{
        private readonly MvcContext _context;

        public SocioController(MvcContext cont){
            _context=cont;
        }

        public IActionResult RegistrarSocio(){

            return View();
        }

        public IActionResult ModificarSocio(){

            return View();
        }


        public IActionResult ConfirmacionSocio(){

            return View();
        }


        

    }



}