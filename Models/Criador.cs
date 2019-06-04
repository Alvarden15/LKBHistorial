using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    public class Criador
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required (ErrorMessage="Por favor, rellene los datos de aqui")]
        [Column("nombre")]
        public string Nombre { get; set; }
        [Required (ErrorMessage="Por favor, rellene los datos de aqui")]
        [Column("apellido_paterno")]
        public string ApellidoPaterno { get; set; }
        [Required (ErrorMessage="Por favor, rellene los datos de aqui")]
        [Column("apellido_materno")]
        public string ApellidoMaterno { get; set; }

        [Required (ErrorMessage="Por favor, rellene los datos de aqui")]
        [Column("telefono")]
        public string Telefono{get; set;}

        [InverseProperty("CriadorActual")]
        public virtual List<Perro> PerroActual{get; set;}
        
        [InverseProperty("CriadorOriginal")]
        public virtual List<Perro> PerroOriginal{get; set;}

        //Esto evitará que se duplique los datos al momento de registar en una llave foranea
        [ForeignKey("IdCriadero")] 
        public virtual Criadero Criadero{get; set;}

        [Column("id_Criadero")]
        public int IdCriadero{get; set;}      

        public Criador(){
           PerroOriginal= new List<Perro>();
           PerroActual= new List<Perro>();
        }


    }
}