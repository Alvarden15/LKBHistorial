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
        [Required (ErrorMessage="Rellene el campo de microchip")]
        [Column("id")]
       
        public int Id { get; set; }

        [ForeignKey("IdRaza")]
        public virtual RazaPerro RazaPerro { get; set; }

        [Column("id_raza")]
        public int IdRaza { get; set; }

        [Column("sexo")]
        [Required(ErrorMessage="Por favor, selecciona una de las opciones")]
        [StringLength(1)]
        public string Sexo { get; set; }

        [Required(ErrorMessage="Asigna al perro una fecha de nacimiento")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode=true)]
        [Column("fecha_nacimiento", TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage="Por favor, asigna el nombre")]
        [Column("nombre")]
        [StringLength(20,ErrorMessage="El nombre del perro no puede tener más de 20 caracteres")]
        public string Nombre {get; set;}
        
        [Required(ErrorMessage="Define si es cachorro o adulto")]
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
         [Column("id_padre")]
        public int? IdPadre { get; set; }
        [Column("id_madre")]
        public int? IdMadre { get; set; }
        /*
        

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

        [InverseProperty("Padre")]
        public virtual List<Perro> IdentificarPadre{get; set;}

        [ForeignKey("IdPadre")]
        [InverseProperty("IdentificarPadre")]
        public virtual Perro Padre{get; set;}

        [InverseProperty("Madre")]
        public virtual List<Perro> IdentificarMadre{get; set;}

        [ForeignKey("IdMadre")]
        [InverseProperty("IdentificarMadre")]
        public virtual Perro Madre{get; set;}  

        [InverseProperty("PerroAsociado")]
        public virtual PerroSocio Asociacion { get; set; }
        /*
         
         */

        
        public Perro(){
            IdentificarPadre= new List<Perro>();
            IdentificarMadre= new List<Perro>();
            Lunada= new List<Lunada>();
            Prenada= new List<Prenada>();
            /*
           
             */
           

        }

        

    }
}