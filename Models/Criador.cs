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
        [Column("telefono")]
        public string Telefono{get; set;}

        [InverseProperty("CriadorActual")]
        public List<Perro> PerroOriginal{get; set;}
        [InverseProperty("CriadorOriginal")]
        public List<Perro> PerroActual{get; set;}

        public Criadero Criadero{get; set;}

        [Column("id_Criadero")]
        public int IdCriadero{get; set;}      

        public Criador(){
           PerroOriginal= new List<Perro>();
           PerroActual= new List<Perro>();
        }


    }
}