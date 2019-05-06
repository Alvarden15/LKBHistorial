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

        [Column("id_Perro")]
        public int IdPerro { get; set; }
        [Column("id_Prenada")]
        public int IdPrenada { get; set; }
        
        
    }
}