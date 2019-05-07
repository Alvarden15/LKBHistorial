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

        public List<PerroCriadero> PerroCriadero {get; set;}

        public List<DuenoCriadero> DuenoCriadero{get; set;}

        public Criadero(){
            PerroCriadero= new List<PerroCriadero>();
            DuenoCriadero= new List<DuenoCriadero>();
        }
        


    }
}