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
        public int Id { get; set; }

        public TipoMonta TipoMonta{get; set;}

        public int IdMonta {get; set;}

        public int CantidadInseminadas { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public TipoParto TipoParto { get; set; }

        public int IdParto{ get; set; }

        public String FechaCesaria { get; set; }

        public int NumeroCamadas { get; set; }

        public List<PerroPrenada> PerroPrenada { get; set; }

        public Prenada(){
            PerroPrenada= new List<PerroPrenada>();
        }

    }
}