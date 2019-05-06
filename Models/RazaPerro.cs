using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class RazaPerro
    {
        [Key]
        public int Id { get; set; }

        public string Raza { get; set; }

        public List<Perro> Perro {get; set;}

        public RazaPerro(){
            Perro=new List<Perro>();
        }

    }
}