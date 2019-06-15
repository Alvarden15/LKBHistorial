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
        [Required(ErrorMessage="Por favor, elige aqui quien va a ser el socio")]
        public int Id { get; set; }
        [Required(ErrorMessage="Por favor, escriba aqui el departamente donde pertenece")]
        [Column("departamento")]
        [StringLength(20)]
        public string Departamento { get; set; }
        [Required(ErrorMessage="Por favor, escriba aqui el distrito donde pertenece")]
        [Column("distrito")]
        [StringLength(20)]
        public string Distrito { get; set; }
        [Required (ErrorMessage="Por favor, escriba aqui la calle donde pertenece")]
        [Column("calle")]
        [StringLength(30)]
        public string Calle { get; set; }
        [Required(ErrorMessage="Por favor, escriba aqui el pais donde pertenece")]
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

