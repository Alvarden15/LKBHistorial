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

        public Perro_Adulto Perro_Adulto { get; set; }

        public Raza_Perro Raza_Perro { get; set; }

        public int Raza_PerroID { get; set; }

        public List<Reproductora> Reproductora { get; set; }

        public List<Perro_Criadero> Perro_Criadero{get; set;}

        public String Sexo { get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString="{0:dd/mm/yyyy}",ApplyFormatInEditMode=true)]
        public DateTime FechaNacimiento { get; set; }
        
        

        public Perro(){
            Reproductora=new List<Reproductora>();
            Perro_Criadero=new List<Perro_Criadero>();
        }

        

    }
}