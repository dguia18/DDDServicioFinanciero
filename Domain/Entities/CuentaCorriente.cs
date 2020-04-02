using System;
using System.Collections.Generic;

namespace Domain.Entities
{
	public class CuentaCorriente : ServicioFinanciero
	{
		public double CupoDeSobregiro { get; set; }
		public const double VALOR_MINIMO_CONSIGNACION_INICIAL = 10000;
		private const double CUATRO_X_MIL = 4 / 1000;
		public override IList<string> CanConsign(double valor)
		{
			var errores = new List<string>();
			if (this.GetConsignaciones().Count == 0 && valor < VALOR_MINIMO_CONSIGNACION_INICIAL)
				errores.Add($"No es posible realizar la consignacion," +
					$" el monto minimo debe ser de: {VALOR_MINIMO_CONSIGNACION_INICIAL}");
			if (valor <= 0)
				errores.Add("El valor a consignar es incorrecto");
			return errores;
		}
		public override string Consignar(double valor, string ciudadDeOrigen)
		{
			if (CanConsign(valor).Count > 0)
				throw new InvalidOperationException();
			this.CrearMovimientoDeIngreso(valor);
			return ($"Su Nuevo Saldo es de ${this.Saldo} pesos");
		}
		public override IList<string> CanWithDraw(double valor)
		{
			var errores = new List<string>();
			double nuevoSaldo = this.Saldo - valor;
			if (nuevoSaldo < CupoDeSobregiro)
				errores.Add($"No es posible realizar el retiro, su saldo es menor al cupo " +
					$"de sobregiro contratado:{this.CupoDeSobregiro}");
			if (valor <= 0)
				errores.Add("El valor a retirar es incorrecto");
			return errores;
		}
		public override string Retirar(double valor)
		{
			valor = DebitarCuatroXMil(valor);
			this.CrearMovimientoDeEgreso(valor);
			return ($"Su Nuevo Saldo es de ${this.Saldo} pesos");
		}
		private double DebitarCuatroXMil(double valor)
		{
			return valor * (1 - CUATRO_X_MIL);
		}
	}

	[Serializable]
	public class CuentaCorrienteRetirarMaximoSobregiroException : Exception
	{
		public CuentaCorrienteRetirarMaximoSobregiroException() { }
		public CuentaCorrienteRetirarMaximoSobregiroException(string message) : base(message) { }
		public CuentaCorrienteRetirarMaximoSobregiroException(string message, Exception inner) : base(message, inner) { }
		protected CuentaCorrienteRetirarMaximoSobregiroException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
