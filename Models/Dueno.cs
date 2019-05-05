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

        public String Apellido_paterno { get; set; }

        public String Apellido_materno { get; set; }


    }
}