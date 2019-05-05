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

        public List<Perro_Criadero> Perro_Criadero {get; set;}

        public Criadero(){
            Perro_Criadero= new List<Perro_Criadero>();
        }
        


    }
}