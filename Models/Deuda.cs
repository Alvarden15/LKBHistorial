using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace LKBHistorial.Models{
    [Table("deuda")]
    public class Deuda{
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("id_deudor")]
        public int IdDeudor { get; set; }
        [Column("cuotas")]
        public int Cuotas { get; set; }
        [Column("monto_inicial")]
        public int MontoInicial { get; set; }
        [Column("monto_total")]
        public int MontoTotal { get; set; }
        [Column("numero_cuotas")]
        public byte NumeroCuotas { get; set; }
        [Column("saldo_pendiente")]
        public int SaldoPendiente { get; set; }
        [Column("cuotas_pendientes")]
        public byte CuotasPendientes { get; set; }
        [Column("fecha_inicio", TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [ForeignKey("IdDeudor")]
        [InverseProperty("Deuda")]
        public virtual Deudor IdDeudorNavigation { get; set; }

    }

}