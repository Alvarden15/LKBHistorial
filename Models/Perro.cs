using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    public class Perro
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        public PerroAdulto PerroAdulto { get; set; }
        
        [ForeignKey("IdRaza")]
       
        public RazaPerro Raza { get; set; }

        [Column("id_raza")]
        public int IdRazaPerro { get; set; }

        public List<PerroCriadero> PerroCriadero{get; set;}

        public List<PerroLunada> PerroLunada{get;set;}

        public List<PerroPrenada> PerroPrenada { get; set; }
        [Required]
        [Column("dueno_actual")]
        public string DuenoActual { get; set; }

        [Column("sexo")]
        [Required]
        public string Sexo { get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString="{0:dd/mm/yyyy}",ApplyFormatInEditMode=true)]
        [Column("fecha_nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        
        

        public Perro(){
            
            PerroCriadero=new List<PerroCriadero>();
            PerroLunada= new List<PerroLunada>();
            PerroPrenada= new List<PerroPrenada>();
        }

        

    }
}