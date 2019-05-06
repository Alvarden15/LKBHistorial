using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    public class Dueno
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

        public List<DuenoCriadero> DuenoCriadero{get; set;}

        public Dueno(){
            DuenoCriadero= new List<DuenoCriadero>();
        }


    }
}