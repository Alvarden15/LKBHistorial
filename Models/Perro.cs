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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [ForeignKey("IdRaza")]
        public virtual RazaPerro RazaPerro { get; set; }

        [Column("id_raza")]
        public int IdRaza { get; set; }

        [Column("sexo")]
        [Required]
        [StringLength(1)]
        public string Sexo { get; set; }

        [Required]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode=true)]
        [Column("fecha_nacimiento", TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [Column("nombre")]
        [StringLength(20,ErrorMessage="El nombre del perro no puede tener más de 20 caracteres")]
        public string Nombre {get; set;}
        
        [Required]
        [Column("madurez")]
        [StringLength(8)]
        public string Madurez { get; set; }

        [Column("temperamento")]
        [StringLength(50)]
        public string Temperamento { get; set; }

        [Column("id_Tipo_Estatura")]
        public int? IdEstatura { get; set; }
        [Column("id_Criador_Actual")]
        public int IdCriadorActual { get; set; }

        [Column("id_Criador_Original")]
        public int IdCriadorOriginal { get; set; }

        [Column("id_Criadero")]
        public int IdCriadero{ get; set; }
        /*
         [Column("id_Perro_Padre")]
        public int IdPadre { get; set; }
        [Column("id_Perro_Madre")]
        public int IdMadre { get; set; }

         */
       
        [ForeignKey("IdCriadero")]
        public virtual Criadero Criadero{get; set;}

        [ForeignKey("IdCriadorActual")]
        [InverseProperty("PerroActual")]
        public virtual Criador CriadorActual{get;set;}

        [ForeignKey("IdCriadorOriginal")]
        [InverseProperty("PerroOriginal")]
        public virtual Criador CriadorOriginal{get; set;}

        [ForeignKey("IdEstatura")]
        public virtual TipoEstatura TipoEstatura{get; set;}

        public virtual List<Lunada> Lunada{ get; set;}

        public virtual List<Prenada> Prenada{get; set;}

        /*
         [InverseProperty("Padre")]
        public List<Perro> IdentificarPadre{get; set;}

        [ForeignKey("IdPadre")]
        [InverseProperty("IdentificarPadre")]
        public Perro Padre{get; set;}

        [InverseProperty("Madre")]
        public List<Perro> IdentificarMadre{get; set;}

        [ForeignKey("IdMadre")]
        [InverseProperty("IdentificarMadre")]
        public Perro Madre{get; set;}  
         */

        
        public Perro(){
            Lunada= new List<Lunada>();
            Prenada= new List<Prenada>();
            /*
            IdentificarPadre= new List<Perro>();
            IdentificarMadre= new List<Perro>();
             */
           

        }

        

    }
}