using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class Raza_Perro
    {
        [Key]
        public int ID { get; set; }

        public String Raza { get; set; }

        public List<Perro> Perro {get; set;}

        public Raza_Perro(){
            Perro=new List<Perro>();
        }

    }
}