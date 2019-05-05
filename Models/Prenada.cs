using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class Prenada
    {
        public int ID { get; set; }

        public Tipo_Monta Tipo_Monta{get; set;}

        public int Tipo_MontaID {get; set;}

        public int Cantidad_inseminadas { get; set; }

        public String Fecha_inicio { get; set; }

        public String Fecha_fin { get; set; }

        public Tipo_Parto Tipo_Parto { get; set; }

        public int Tipo_PartoID { get; set; }

        public String FechaCesaria { get; set; }

        public int Numero_camadas { get; set; }

        public List<Perro_Prenada> Perro_Prenada { get; set; }

        public Prenada(){
            Perro_Prenada= new List<Perro_Prenada>();
        }

    }
}