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

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Dueno_Criadero>().HasKey(sc=>new{sc.CriaderoID, sc.DuenoID});
            modelBuilder.Entity<Perro_Criadero>().HasKey(sc=>new{sc.CriaderoID,sc.PerroID});
            modelBuilder.Entity<Perro_Prenada>().HasKey(sc=>new{sc.PerroID,sc.PrenadaID});
            modelBuilder.Entity<Perro_Lunada>().HasKey(sc=>new{sc.PerroID,sc.LunadaID});

        }


        public DbSet<Celo> Celo { get; set; }

        public DbSet<Perro> Perro{get;set;}

        public DbSet<Deudor> Deudor { get; set; }

        public DbSet<Dueno> Dueno{get; set;}

        public DbSet<Criadero> Criadero { get; set; }

        public DbSet<Perro_Adulto> Perro_Adulto { get; set; }

        public DbSet<Lunada> Lunada { get; set; }

        public DbSet<Prenada> Prenada{get; set;}

        public DbSet<Raza_Perro> Raza_Perro{get; set;}

        public DbSet<Tipo_Monta> Tipo_Monta { get; set; }

        public DbSet<Tipo_Parto> Tipo_Parto { get; set; }
        

    }
}