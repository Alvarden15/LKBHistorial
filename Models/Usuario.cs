using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
 [Table("Usuario")]
    public class Usuario{
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("Nombre")]
        [Required]
        public string Nombre { get; set; }

        [Column("Email")]
        [Required]
        public string Email { get; set; }

        [Column("Password")]
        [Required]
        public string Password { get; set; }


        [Column("Telefono")]
        [Required]
        public int Telefono { get; set; }

    }

}
