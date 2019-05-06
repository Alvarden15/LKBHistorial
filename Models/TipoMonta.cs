using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    public class TipoMonta
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("tipo")]
        public string Tipo { get; set; }

        public List<Prenada> Prenada{get; set;}

        public TipoMonta(){
            Prenada= new List<Prenada>();
        }

    }
}