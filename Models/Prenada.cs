using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    public class Prenada
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("id_monta")]
        public int IdMonta {get; set;}
        [Column("cantidad_inseminadas")]
        public int CantidadInseminadas { get; set; }
        [Column("fecha_inicio", TypeName = "date")]
         [DisplayFormat(DataFormatString="{0:dd/mm/yyyy}",ApplyFormatInEditMode=true)]
        public DateTime FechaInicio { get; set; }
        [Column("fecha_fin", TypeName = "date")]
         [DisplayFormat(DataFormatString="{0:dd/mm/yyyy}",ApplyFormatInEditMode=true)]
        public DateTime FechaFin { get; set; }

        [Column("fecha_cesarea", TypeName = "date")]
        public DateTime FechaCesaria { get; set; }
        [Column("numero_camadas")]
        public int NumeroCamadas { get; set; }


        public Prenada(){
            
        }

    }
}