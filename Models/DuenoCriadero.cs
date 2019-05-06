using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class DuenoCriadero
    {

        public Dueno Dueno { get; set; }

        public Criadero Criadero { get; set; }

        public int IdDueno { get; set; }
        
        public int IdCriadero { get; set; }
        
    }
}