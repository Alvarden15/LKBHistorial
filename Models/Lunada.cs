using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class Lunada
    {
        public int ID { get; set; }

        public String Fecha_inicio { get; set; }

        public String Fecha_fin { get; set; }

        public int Numero_celos { get; set; }

        public List<PerroLunada> PerroLunada { get; set; }

        public Lunada(){
            PerroLunada= new List<PerroLunada>();
        }


    }
}