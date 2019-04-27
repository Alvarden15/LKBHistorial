using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;


namespace LKBHistorial.Models
{
    public class Perro
    {
        [Key]
        public String IDPerro { get; set; }

        [Required]
        
        public String NombrePerro { get; set; }

         [Required]
        public int Edad { get; set; }

        [Required]
        public string TipoPerro{get;set;}

        public String Padre { get; set; }

        public String Madre {get; set;}

        public String  AbueloPaterno { get; set; }

        

    }
}