using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
	public class TarjetaDeCredito : ServicioFinanciero
	{
		public double CupoPreAprobado { get; set; }
		public void ContratarCupo(double valor)
		{
			this.CupoPreAprobado = valor;
			this.Saldo = valor;
		}
		public override IList<string> CanConsign(double valor)
		{
			var errores = new List<string>();
			if (valor > this.Saldo)
				errores.Add($"El valor del abono no puede superar el saldo de: {this.Saldo}");
			if (valor <= 0)
				errores.Add("El valor a abonar es incorrecto");

			return errores;
		}
		public override string Consignar(double valor, string ciudadDeOrigen)
		{
			if (CanConsign(valor).Count > 0)
				throw new InvalidOperationException();
			return EjecutarAbono(valor);
		}		
		private string EjecutarAbono(double valor)
		{
			this.CupoPreAprobado += valor;
			this.CrearMovimientoDeIngreso(valor);
			this.Saldo -= 2 * valor;
			return ($"Su Nuevo Saldo es de ${this.Saldo} pesos");
		}
		public override IList<string> CanWithDraw(double valor)
		{
			var errores = new List<string>();
			TimeSpan time = DateTime.Now - this.FechaCreacion;
			int restoDias = time.Days;
			if (valor > CupoPreAprobado)
				errores.Add($"El valor a avanzar no puede ser mayor al " +
					$"cupo pre-aprobado: {this.CupoPreAprobado}");
			if (valor <= 0)
				errores.Add("El valor a avanzar es incorrecto");

			return errores;
		}
		public override string Retirar(double valor)
		{
			if (CanWithDraw(valor).Count > 0)
				throw new InvalidOperationException();
			return EjecutarAvance(valor);
		}		
		private string EjecutarAvance(double valor)
		{
			this.CupoPreAprobado -= valor;
			this.Saldo += 2 * valor;
			this.CrearMovimientoDeEgreso(valor);
			return ($"Su Nuevo Saldo es de ${this.Saldo} pesos");
		}
	}
}
