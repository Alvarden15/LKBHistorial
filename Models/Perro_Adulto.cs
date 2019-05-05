using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class Perro_Adulto
    {
        public Perro Perro { get; set; }

        [Key]
        public int PerroID { get; set; }

        public int Temperatura { get; set; }

        public int Altura { get; set; }

        


    }
}