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
        [Required]
        [Column("nombre")]
        [StringLength(20)]
        public string Nombre { get; set; }
        [Required]
        [Column("sexo")]
        [StringLength(1)]
        public string Sexo { get; set; }
        [Required(ErrorMessage="Ejem....¿A quien estas asignado el perro?")]
        [Column("id_socio")]
        public int IdSocio { get; set; }
        [Required(ErrorMessage="Necesitas de un perro si o si")]
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

