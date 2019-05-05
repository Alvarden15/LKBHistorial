using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class Perro_Criadero
    {
        

        public Criadero Criadero{get; set;}

        public Perro Perro{get; set;}

        public int CriaderoID { get; set; }

        public int PerroID { get; set; }


    }
}