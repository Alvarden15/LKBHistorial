using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class Dueno
    {
        [Key]
        public int ID { get; set; }

        public String Nombre { get; set; }

        public String ApellidoPaterno { get; set; }

        public String ApellidoMaterno { get; set; }

        public string Celular{get; set;}


    }
}