using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    [Table("deudor")]
    public class Deudor
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("correo")]
        [StringLength(25)]
        public string Correo { get; set; }
        [Column("estado")]
        public bool Estado { get; set; }

        [ForeignKey("Id")]
        [InverseProperty("Deudor")]
        public virtual Persona PersonaDeudor { get; set; }
        [InverseProperty("DeudorNavigation")]
        public virtual List<Deuda> Deuda { get; set; }

        public Deudor(){
            Deuda= new List<Deuda>();
        }
    }
}