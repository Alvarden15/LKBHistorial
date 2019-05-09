using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    public class Criadero
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required]
        [Column("departamento")]
        public string Departamento { get; set; }

        [Required]
        [Column("distrito")]
        public string Distrito { get; set; }

        [Required]
        [Column("calle")]
        public string Calle { get; set; }

        public List<Perro> Perro {get; set;}

        public List<Criador> Criador{get; set;}

        public Criadero(){
           Perro=new List<Perro>();
           Criador= new List<Criador>();
        }
        


    }
}