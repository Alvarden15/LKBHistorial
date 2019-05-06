using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    public class PerroPrenada
    {
        [ForeignKey("id_Perro")]
        public Perro Perro { get; set; }

        [ForeignKey("id_prenada")]
        public Prenada Prenada { get; set; }

        public int IdPerro { get; set; }

        public int IdPrenada { get; set; }
        
        
    }
}