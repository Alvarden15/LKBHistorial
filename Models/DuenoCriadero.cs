using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    public class DuenoCriadero
    {
        
        public Dueno Dueno { get; set; }

        public Criadero Criadero { get; set; }

        [Column("id_Perro")]
        public int IdDueno { get; set; }
        [Column("id_Criadero")]
        public int IdCriadero { get; set; }
        
    }
}