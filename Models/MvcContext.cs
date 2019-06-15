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


        public DbSet<Perro> Perro{get;set;}

        public DbSet<Criadero> Criadero { get; set; }

        public DbSet<Criador> Criador {get; set;}

        public DbSet<Lunada> Lunada { get; set; }

        public DbSet<Prenada> Prenada{get; set;}

        public DbSet<RazaPerro> RazaPerro{get; set;}

        public DbSet<TipoEstatura> TipoEstatura{get; set;}

        public DbSet<Socio> Socio{get;set;}

        public DbSet<PerroSocio> PerroSocio { get; set; }
        public DbSet<Persona> Persona { get; set; }

        public DbSet<Deuda> Deuda { get; set; }
        public DbSet<Deudor> Deudor { get; set; }
        
    }
}