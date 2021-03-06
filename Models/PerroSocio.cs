using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models{
    [Table("perro_socio")]
    public class PerroSocio{
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Required(ErrorMessage="Ejem....¿A quien estas asignado el perro? ¿A un fantasma?")]
        [Column("id_socio")]
        public int IdSocio { get; set; }
        [Required(ErrorMessage="Necesitas de un perro si o si ¿Por qué crees que se creo esta pagina?")]
        [Column("id_perro")]
        public int IdPerro { get; set; }

        [ForeignKey("IdSocio")]
        [InverseProperty("PerroSocio")]
        public virtual Socio SocioNavigation { get; set; }

        [InverseProperty("Asociacion")]
        [ForeignKey("IdPerro")]

        public virtual Perro PerroAsociado { get; set; }


    }


}

