using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class PerroPrenada
    {
        public Perro Perro { get; set; }

        public Prenada Prenada { get; set; }

        public int IdPerro { get; set; }

        public int IdPrenada { get; set; }
        
        
    }
}