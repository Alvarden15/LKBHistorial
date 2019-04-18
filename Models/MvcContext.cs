using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace LKBHistorial.Models
{
    public class MvcContext: DbContext
    {
        public MvcContext(DbContextOptions options): base(options){

        }

        public DbSet<Perro> Perro{get;set;}

        public DbSet<Deudor> Deudor { get; set; }
        

    }
}