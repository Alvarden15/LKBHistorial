using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models{
    [Table("socio")]
    public class Socio{
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("departamento")]
        [StringLength(20)]
        public string Departamento { get; set; }
        [Required]
        [Column("distrito")]
        [StringLength(20)]
        public string Distrito { get; set; }
        [Required]
        [Column("calle")]
        [StringLength(30)]
        public string Calle { get; set; }
        [Column("pais")]
        [StringLength(20)]
        public string Pais { get; set; }

        [ForeignKey("Id")]
        [InverseProperty("Socio")]
        public virtual Persona PersonaSocio { get; set; }
        [InverseProperty("SocioNavigation")]
        public virtual List<PerroSocio> PerroSocio { get; set; }
        
        public Socio(){
            PerroSocio= new List<PerroSocio>();
        }
        
    }

}

