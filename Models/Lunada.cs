using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    public class Lunada
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("fecha_inicio", TypeName = "date")]
        [DisplayFormat(DataFormatString="{0:dd/mm/yyyy}",ApplyFormatInEditMode=true)]
        public DateTime FechaInicio { get; set; }

        [Column("fecha_fin", TypeName = "date")]
        [DisplayFormat(DataFormatString="{0:dd/mm/yyyy}",ApplyFormatInEditMode=true)]
        public DateTime FechaFin { get; set; }

        [Column("numero_celos")]
        public int NumeroCelos { get; set; }

        [ForeignKey("IdPerro")]
        public Perro Perro{get; set;}

        [Column("id_Perro")]
        public int IdPerro{get; set;}


    }
}