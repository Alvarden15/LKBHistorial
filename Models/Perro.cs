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

        public List<PerroCriadero> PerroCriadero{get; set;}

        public List<PerroLunada> PerroLunada{get;set;}

        public List<PerroPrenada> PerroPrenada { get; set; }
        
        public String Dueno_actual { get; set; }

        public String Sexo { get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString="{0:dd/mm/yyyy}",ApplyFormatInEditMode=true)]
        public DateTime Fecha_nacimiento { get; set; }
        
        

        public Perro(){
            
            PerroCriadero=new List<PerroCriadero>();
            PerroLunada= new List<PerroLunada>();
            PerroPrenada= new List<PerroPrenada>();
        }

        

    }
}