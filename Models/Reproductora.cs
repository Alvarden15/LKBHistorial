using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;


namespace LKBHistorial.Models
{
    public class Reproductora
    {
        [Key]
        public int IDReproductora { get; set; }

        public Perro Perro { get; set; }

        public int IDPerro{get; set;}

        public DateTime Fecha{get;set;}

        
    }
}