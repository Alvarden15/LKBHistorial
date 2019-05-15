using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    public class Filtro{
        public IEnumerable<Perro> Perro { get; set; }

        public IEnumerable<RazaPerro> RazaPerro{get;set;}

        public IEnumerable<Criadero> Criadero { get; set; }

        
    }

}
