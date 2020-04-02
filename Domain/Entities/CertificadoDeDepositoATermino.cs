using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Entities
{
	public class CertificadoDeDepositoATermino : ServicioFinanciero
	{
		public const double VALOR_CONSIGNACION_INICIAL = 1000000;
		public int DiasDeTermino { get; set; }
		public CertificadoDeDepositoATermino()
		{
			this.DiasDeTermino = 30;
		}
		public override IList<string> CanConsign(double valor)
		{
			var errores = new List<string>();
			TimeSpan time = DateTime.Now - this.FechaCreacion;
			int restoDias = time.Days;
			if (this.GetConsignaciones().Count == 0 && valor < VALOR_CONSIGNACION_INICIAL)
				errores.Add("El valor de la consignacion inicial" +
					$" debe ser de {VALOR_CONSIGNACION_INICIAL}");
			if (this.GetConsignaciones().Count > 0 && restoDias <= DiasDeTermino)
				errores.Add("No es posible realizar una segunda consignacion");
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
			TimeSpan time = DateTime.Now - this.FechaCreacion;
			int restoDias = time.Days;
			if (restoDias <= DiasDeTermino)
				errores.Add($"No es posible retirar antes de los" +
					$"{DiasDeTermino} definidos en el contrato");
			if (valor <= 0)
				errores.Add("El valor a retirar es incorrecto");
			double nuevoSaldo = this.Saldo - valor;
			if (nuevoSaldo < 0)
				errores.Add($"No es posible realizar el retiro por falta de saldo, su saldo: {this.Saldo}");

			return errores;
		}
		public override string Retirar(double valor)
		{
			if (CanWithDraw(valor).Count > 0)
				throw new InvalidOperationException();
			this.CrearMovimientoDeEgreso(valor);
			return ($"Su Nuevo Saldo es de ${this.Saldo} pesos");
		}
		
	}
	[Serializable]
	public class ConsignacionInicialInvalidaException : Exception
	{
		public ConsignacionInicialInvalidaException() : base()
		{ }

		public ConsignacionInicialInvalidaException(string message) : base(message)
		{ }

		public ConsignacionInicialInvalidaException(string message, Exception innerException)
			: base(message, innerException)
		{ }

		public ConsignacionInicialInvalidaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{ }
	}
	[Serializable]
	public class RetiroInvalidoPorTiempoTranscurridoException : Exception
	{
		public RetiroInvalidoPorTiempoTranscurridoException() : base()
		{ }

		public RetiroInvalidoPorTiempoTranscurridoException(string message) : base(message)
		{ }

		public RetiroInvalidoPorTiempoTranscurridoException(string message, Exception innerException)
			: base(message, innerException)
		{ }

		public RetiroInvalidoPorTiempoTranscurridoException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{ }
	}
}