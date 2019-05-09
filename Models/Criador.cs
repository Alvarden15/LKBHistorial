using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    public class Criador
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }
        [Required]
        [Column("apellido_paterno")]
        public string ApellidoPaterno { get; set; }
        [Required]
        [Column("apellido_materno")]
        public string ApellidoMaterno { get; set; }

        [Required]
        [Column("celular")]
        public string Celular{get; set;}

        public List<Perro> Perro{get; set;}      

        public Dueno(){
           Perro= new List<Perro>();
        }


    }
}