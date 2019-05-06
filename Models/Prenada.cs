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
        public int Id { get; set; }

        public TipoMonta TipoMonta{get; set;}
        [Column("id_monta")]
        public int IdMonta {get; set;}
        [Column("cantidad_inseminadas")]
        public int CantidadInseminadas { get; set; }
        [Column("fecha_inicio", TypeName = "date")]
        public DateTime FechaInicio { get; set; }
        [Column("fecha_fin", TypeName = "date")]
        public DateTime FechaFin { get; set; }

        public TipoParto TipoParto { get; set; }
        [Column("id_parto")]
        public int IdParto{ get; set; }
        [Column("fecha_cesarea", TypeName = "date")]
        public DateTime FechaCesaria { get; set; }
        [Column("numero_camadas")]
        public int NumeroCamadas { get; set; }

        public List<PerroPrenada> PerroPrenada { get; set; }

        public Prenada(){
            PerroPrenada= new List<PerroPrenada>();
        }

    }
}