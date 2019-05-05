using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class Perro_Prenada
    {
        public Perro Perro { get; set; }

        public Prenada Prenada { get; set; }

        public int PerroID { get; set; }

        public int PrenadaID { get; set; }
        
        
    }
}