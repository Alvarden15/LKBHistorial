using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class Tipo_Monta
    {
        [Key]
        public int ID { get; set; }

        public String Tipo { get; set; }


    }
}