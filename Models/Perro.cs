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

        public String Temperamiento { get; set; }

        public int Microchip {get; set;}

        [Required]
        [DisplayFormat(DataFormatString="{0:dd/mm/yyyy}",ApplyFormatInEditMode=true)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public String TipoPerro{get;set;}

        [Required]
        public String Raza{get; set;}

        public String Estatura { get; set; }
        
        public String Criadero { get; set; }

        public String Padre { get; set; }

        public String Madre {get; set;}

        public List<Reproductora> Reproductora { get; set; }
        
        public String Imagen{get; set;}

        public Perro(){
            Reproductora=new List<Reproductora>();
        }

        

    }
}