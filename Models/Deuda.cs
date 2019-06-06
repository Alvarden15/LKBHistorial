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
        [Required (ErrorMessage="Por favor, escribe aqui el valor de las cuotas")]
        public int Cuotas { get; set; }
        [Column("monto_inicial")]
        [Required (ErrorMessage="Por favor, escribe aqui el monto inicial")]
        public int MontoInicial { get; set; }
        [Column("monto_total")]
        [Required (ErrorMessage="Por favor, escribe aqui el monto total")]
        public int MontoTotal { get; set; }
        [Column("numero_cuotas")]
        [Required (ErrorMessage="Por favor, escribe aqui la cantidad de cuotas")]
        public int NumeroCuotas { get; set; }
        [Column("saldo_pendiente")]
        [Required (ErrorMessage="Por favor, escribe aqui el saldo pendiente de la deuda")]
        public int SaldoPendiente { get; set; }
        [Column("cuotas_pendientes")]
        [Required (ErrorMessage="Por favor, escribe aqui las cuotas que faltan")]
        public int CuotasPendientes { get; set; }

        [Column("fecha_inicio", TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode=true)]
        [Required (ErrorMessage="Por favor, defina cuando empezó la deuda")]
        public DateTime FechaInicio { get; set; }

        [ForeignKey("IdDeudor")]
        [InverseProperty("Deuda")]
        public virtual Deudor DeudorNavigation { get; set; }

    }

}