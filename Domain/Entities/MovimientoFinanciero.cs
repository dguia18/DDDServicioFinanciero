using Domain.Base;
using System;

namespace Domain.Entities
{
	public class MovimientoFinanciero : Entity<int>
	{
		public ServicioFinanciero CuentaBancaria { get; set; }
		public double ValorRetiro { get; set; }
		public double ValorConsignacion { get; set; }
		public DateTime FechaMovimiento { get; set; }
	}
}
