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
        
        [Column("cantidad_inseminadas")]
        public int CantidadInseminadas { get; set; }

        [Column("fecha_inicio", TypeName = "date")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode=true)]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Column("fecha_fin", TypeName = "date")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode=true)]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        [Required]
        [Column("tipo_parto")]
        [StringLength(12)]
        public string TipoParto { get; set; }

        [Column("fecha_cesarea", TypeName = "date")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode=true)]
        [DataType(DataType.Date)]
        public DateTime? FechaCesaria { get; set; }

        [Column("fecha_parto_normal", TypeName = "date")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode=true)]
        [DataType(DataType.Date)]
        public DateTime? FechaPartoNormal { get; set; }

        [Column("numero_camadas")]
        public int NumeroCamadas { get; set; }

        [ForeignKey("IdPerro")]
        public virtual Perro Perro{get; set;}

        [Column("id_Perro")]
        public int IdPerro{get; set;}


    }
}