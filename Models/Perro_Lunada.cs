using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class Perro_Lunada
    {
        public Perro Perro { get; set; }

        public Lunada Lunada { get; set; }

        public int PerroID { get; set; }

        public int LunadaID { get; set; }

        
    }
}