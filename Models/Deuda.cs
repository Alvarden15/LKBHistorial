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
        [Required]
        public int Cuotas { get; set; }
        [Column("monto_inicial")]
        [Required]
        public int MontoInicial { get; set; }
        [Column("monto_total")]
        [Required]
        public int MontoTotal { get; set; }
        [Column("numero_cuotas")]
        [Required]
        public int NumeroCuotas { get; set; }
        [Column("saldo_pendiente")]
        [Required]
        public int SaldoPendiente { get; set; }
        [Column("cuotas_pendientes")]
        [Required]
        public int CuotasPendientes { get; set; }
        [Column("fecha_inicio", TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode=true)]
        [Required]
        public DateTime FechaInicio { get; set; }

        [ForeignKey("IdDeudor")]
        [InverseProperty("Deuda")]
        public virtual Deudor DeudorNavigation { get; set; }

    }

}