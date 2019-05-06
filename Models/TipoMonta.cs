using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class TipoMonta
    {
        [Key]
        public int Id { get; set; }

        public string Tipo { get; set; }

        public List<Prenada> Prenada{get; set;}

        public TipoMonta(){
            Prenada= new List<Prenada>();
        }

    }
}