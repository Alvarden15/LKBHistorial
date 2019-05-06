using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    public class PerroLunada
    {
        public Perro Perro { get; set; }

        public Lunada Lunada { get; set; }
        [Column("id_Perro")]
        public int IdPerro { get; set; }
        [Column("id_Lunada")]
        public int IdLunada { get; set; }

        
    }
}