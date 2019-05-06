using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class Criadero
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public String Nombre { get; set; }

        [Required]
        public String Departamento { get; set; }

        [Required]
        public String Distrito { get; set; }

        [Required]
        public String Calle { get; set; }

        public List<PerroCriadero> PerroCriadero {get; set;}

        public Criadero(){
            PerroCriadero= new List<PerroCriadero>();
        }
        


    }
}