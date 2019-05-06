using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class PerroAdulto
    {
        public Perro Perro { get; set; }

        [Key]
        public int Id { get; set; }

        public string Temperamento{ get; set; }

        public string Estatura { get; set; }

        


    }
}