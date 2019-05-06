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
            modelBuilder.Entity<DuenoCriadero>().HasKey(sc=>new{sc.IdCriadero, sc.IdDueno});
            modelBuilder.Entity<PerroCriadero>().HasKey(sc=>new{sc.IdCriadero,sc.IdPerro});
            modelBuilder.Entity<PerroPrenada>().HasKey(sc=>new{sc.IdPerro,sc.IdPrenada});
            modelBuilder.Entity<PerroLunada>().HasKey(sc=>new{sc.IdPerro,sc.IdLunada});

        }


        public DbSet<Perro> Perro{get;set;}

        public DbSet<Deudor> Deudor { get; set; }

        public DbSet<Dueno> Dueno{get; set;}

        public DbSet<Criadero> Criadero { get; set; }

        public DbSet<PerroAdulto> PerroAdulto { get; set; }

        public DbSet<Lunada> Lunada { get; set; }

        public DbSet<Prenada> Prenada{get; set;}

        public DbSet<RazaPerro> RazaPerro{get; set;}

        public DbSet<TipoMonta> TipoMonta { get; set; }

        public DbSet<TipoParto> TipoParto { get; set; }
        

    }
}