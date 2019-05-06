using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class PerroCriadero
    {
        

        public Criadero Criadero{get; set;}

        public Perro Perro{get; set;}

        public int IdCriadero{ get; set; }

        public int IdPerro { get; set; }


    }
}