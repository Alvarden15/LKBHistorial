using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    [Table("Raza_Perro")]
    public class RazaPerro
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("raza")]
        [StringLength(20)]
        public string Raza { get; set; }

        
        public virtual List<Perro> Perro {get; set;}

        public RazaPerro(){
            Perro=new List<Perro>();
        }

    }
}