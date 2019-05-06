using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    public class PerroAdulto
    {
        [ForeignKey("Id")]
        
        public Perro Perro { get; set; }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("temperamento")]
        public string Temperamento{ get; set; }

        [Required]
        [Column("estatura")]
        public string Estatura { get; set; }

        


    }
}