using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    public class Criadero
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        [StringLength(20, ErrorMessage="El nombre del criadero no puede tener más de 20 caracteres")]
        public string Nombre { get; set; }

        [Required]
        [Column("departamento")]
        [StringLength(20,ErrorMessage="El nombre del departamento no puede tener más de 20 caracteres")]
        public string Departamento { get; set; }

        [Required]
        [Column("distrito")]
        [StringLength(20, ErrorMessage="El nombre del distrito no puede tener más de 20 caracteres")]
        public string Distrito { get; set; }

        [Required]
        [Column("calle")]
        [StringLength(30, ErrorMessage="El nombre de la calle no puede tener más de 30 caracteres")]
        public string Calle { get; set; }

        public virtual List<Perro> Perro {get; set;}

        public virtual List<Criador> Criador{get; set;}

        public Criadero(){
           Perro=new List<Perro>();
           Criador= new List<Criador>();
        }
        


    }
}