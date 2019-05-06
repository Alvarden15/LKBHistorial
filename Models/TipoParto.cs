using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class TipoParto
    {
        [Key]
        public int ID { get; set; }

        public string Tipo { get; set; }

        public List<Prenada> Prenada{get; set;}

        public TipoParto(){
            Prenada= new List<Prenada>();
        }

    }
}