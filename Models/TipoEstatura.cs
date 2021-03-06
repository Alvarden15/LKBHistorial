using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    [Table("Tipo_Estatura")]
    public class TipoEstatura
    {

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("tipo_estatura")]
        [StringLength(10)]
        public string Estatura { get; set; }

        public virtual List<Perro> Perro{get; set;}


        public TipoEstatura(){
            Perro= new List<Perro>();
        }
    }
}