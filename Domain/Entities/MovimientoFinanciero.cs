using Domain.Base;
using System;

namespace Domain.Entities
{
    public class MovimientoFinanciero : Entity<int>
    {
        public ServicioFinanciero CuentaBancaria { get; set; }
        public decimal ValorRetiro { get; set; }
        public decimal ValorConsignacion { get; set; }
        public DateTime FechaMovimiento { get; set; }
    }
}
