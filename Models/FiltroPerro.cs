using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LKBHistorial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.MvcContext;

namespace LKBHistorial.Models
{
    public class FiltroPerro
    {
        public List<Perro> Perro{get;set;}

        public String nombre {get; set;}

        public String familiar{get; set;}
    }
}