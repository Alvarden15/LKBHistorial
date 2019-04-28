using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LKBHistorial.Models;

namespace Models.MvcContext
{
    public class MvcContext: DbContext
    {
        public MvcContext(DbContextOptions<MvcContext> options): base(options){

        }



        public DbSet<Celo> Celo { get; set; }

        public DbSet<Perro> Perro{get;set;}
        public DbSet<Reproductora> Reproductora { get; set; }

        public DbSet<Deudor> Deudor { get; set; }
        

    }
}