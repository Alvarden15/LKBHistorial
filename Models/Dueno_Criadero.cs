using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class Dueno_Criadero
    {

        public Dueno Dueno { get; set; }

        public Criadero Criadero { get; set; }

        public int DuenoID { get; set; }
        
        public int CriaderoID { get; set; }
        
    }
}