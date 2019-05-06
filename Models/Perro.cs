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
        public int Id { get; set; }

        [Required]
        
        public String NombrePerro { get; set; }

        public PerroAdulto PerroAdulto { get; set; }

        public RazaPerro RazaPerro { get; set; }

        public int Raza_PerroID { get; set; }

        public List<Perro_Criadero> Perro_Criadero{get; set;}

        public List<Perro_Lunada> Perro_Lunada{get;set;}

        public List<Perro_Prenada> Perro_Prenada { get; set; }
        
        public String Dueno_actual { get; set; }

        public String Sexo { get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString="{0:dd/mm/yyyy}",ApplyFormatInEditMode=true)]
        public DateTime Fecha_nacimiento { get; set; }
        
        

        public Perro(){
            
            Perro_Criadero=new List<Perro_Criadero>();
            Perro_Lunada= new List<Perro_Lunada>();
            Perro_Prenada= new List<Perro_Prenada>();
        }

        

    }
}