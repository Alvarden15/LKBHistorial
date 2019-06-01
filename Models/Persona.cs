using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    [Table("persona")]
    public class Persona{
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nombre")]
        [StringLength(20)]
        public string Nombre { get; set; }
        [Required]
        [Column("apellido_paterno")]
        [StringLength(20)]
        public string ApellidoPaterno { get; set; }
        [Required]
        [Column("apellido_materno")]
        [StringLength(20)]
        public string ApellidoMaterno { get; set; }

        [InverseProperty("IdNavigation")]
        public virtual Deudor Deudor { get; set; }
        [InverseProperty("IdNavigation")]
        public virtual Socio Socio { get; set; }


    }


}