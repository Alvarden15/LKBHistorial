using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    public class Perro
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public RazaPerro RazaPerro { get; set; }

        [Column("id_raza")]
        public int IdRaza { get; set; }

        [Column("sexo")]
        [Required]
         [StringLength(1)]
        public string Sexo { get; set; }

        [Required]
        [DisplayFormat(DataFormatString="{0:dd/mm/yyyy}",ApplyFormatInEditMode=true)]
        [Column("fecha_nacimiento", TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [Column("nombre")]
        [StringLength(20)]
        public string Nombre {get; set;}
        
        [Required]
        [Column("madurez")]
        [StringLength(8)]
        public string Madurez { get; set; }

        [Column("temperamento")]
        [StringLength(50)]
        public string Temperamento { get; set; }

        [Column("id_Tipo_Estatura")]
        public int IdEstatura { get; set; }
        [Column("id_Criador_Actual")]
        public int IdCriadorActual { get; set; }

        [Column("id_Criador_Original")]
        public int IdCriadorOriginal { get; set; }

        [Column("id_Criadero")]
        public int IdCriadero{ get; set; }
        [Column("id_Perro_Padre")]
        public int IdPadre { get; set; }
        [Column("id_Perro_Madre")]
        public int IdMadre { get; set; }

        public Criadero Criadero{get; set;}

        [InverseProperty("PerroActual")]
        public Criador CriadorActual{get;set;}
        
        [InverseProperty("PerroOriginal")]
        public Criador CriadorOriginal{get; set;}

        public TipoEstatura TipoEstatura{get; set;}

        public List<Lunada> Lunada{ get; set;}

        public List<Prenada> Prenada{get; set;}

        [InverseProperty("Padre")]
        public List<Perro> IdentificarPadre{get; set;}


        [InverseProperty("IdentificarPadre")]
        public Perro Padre{get; set;}

        [InverseProperty("Madre")]
        public List<Perro> IdentificarMadre{get; set;}
        
        [InverseProperty("IdentificarMadre")]
        public Perro Madre{get; set;}

        
        
        public Perro(){
            Lunada= new List<Lunada>();
            Prenada= new List<Prenada>();
            IdetificarPadre= new List<Perro>();
            IdentificarMadre= new List<Perro>();

        }

        

    }
}