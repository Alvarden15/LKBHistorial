using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LKBHistorial.Models;
using Microsoft.EntityFrameworkCore;



namespace LKBHistorial.Controllers
{
    public class PerroController:Controller
    {
        readonly MvcContext context;

        public PerroController(MvcContext cont){
            context=cont;
        }
        

    }
}