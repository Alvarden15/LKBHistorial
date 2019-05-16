using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKBHistorial.Models
{
    public class Filtro{
        public IEnumerable<Perro> Perro { get; set; }

        public IEnumerable<RazaPerro> RazaPerro{get;set;}

        public IEnumerable<Criadero> Criadero { get; set; }

        public IEnumerable<Criador> Criador { get; set; }

        public IEnumerable<Prenada> Prenada { get; set; }

        public IEnumerable<Lunada> Lunada { get; set; }

        public IEnumerable<TipoEstatura> TipoEstatura { get; set; }
        
    }

}
