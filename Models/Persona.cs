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
        [Required (ErrorMessage="Por favor, escribe aqui el nombre")]
        [Column("nombre")]
        [StringLength(20, ErrorMessage="Lo sentimos, el nombre no debe tener màs de 20 caracteres")]
        public string Nombre { get; set; }
        [Required (ErrorMessage="Por favor, escribe aqui el apellido paterno")]
        [Column("apellido_paterno")]
        [StringLength(20,ErrorMessage="Lo sentimos, el apellido paterno no debe tener màs de 20 caracteres")]
        public string ApellidoPaterno { get; set; }
        [Required (ErrorMessage="Por favor, escribe aqui el apellido paterno")]
        [Column("apellido_materno")]
        [StringLength(20,ErrorMessage="Lo sentimos, el apellido materno no debe tener màs de 20 caracteres")]
        public string ApellidoMaterno { get; set; }

        [InverseProperty("PersonaDeudor")]
        public virtual Deudor Deudor { get; set; }
        [InverseProperty("PersonaSocio")]
        public virtual Socio Socio { get; set; }


    }


}