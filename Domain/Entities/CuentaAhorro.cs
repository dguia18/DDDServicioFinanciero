using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Entities
{
	public class CuentaAhorro : ServicioFinanciero
	{
		private const int CANTIDAD_DE_RETIROS_SIN_COSTO = 3;
		private const double COSTO_POR_RETIRO = 5000;
		private const int DESCUENTO_POR_SUCURSAL_EN_OTRA_CIUDAD = 10000;
		public const double VALOR_CONSIGNACION_INICIAL = 50000;
		public const double SALDO_MINIMO = 20000;
		
	
		public override IList<string> CanConsign(double valor)
		{
			var errores = new List<string>();
			if (this.GetConsignaciones().Count == 0 && valor < VALOR_CONSIGNACION_INICIAL)
				errores.Add("El valor mínimo de la primera consignación debe ser" +
							$"de ${VALOR_CONSIGNACION_INICIAL} mil pesos.");
			if (valor <= 0)
				errores.Add("El valor a consignar es incorrecto");
			return errores;
		}
		public override string Consignar(double valor, string ciudadDeOrigen)
		{

			if (CanConsign(valor).Count != 0)
				throw new InvalidOperationException();
			valor = this.IncluirCostoPorCiudadDiferente(valor, ciudadDeOrigen);
			this.CrearMovimientoDeIngreso(valor);
			return ($"Su Nuevo Saldo es de ${this.Saldo} pesos");
		}		
		private double IncluirCostoPorCiudadDiferente(double valor, string ciudadDeOrigen)
		{
			return !ciudadDeOrigen.Equals(this.Ciudad) ? valor - DESCUENTO_POR_SUCURSAL_EN_OTRA_CIUDAD : valor;
		}
		public override IList<string> CanWithDraw(double valor)
		{
			var errores = new List<string>();
			double nuevoSaldo = Saldo - valor;
			if (nuevoSaldo < SALDO_MINIMO)
				errores.Add($"No es posible realizar el Retiro, el nuevo saldo es menor al minimo, ${SALDO_MINIMO}");
			if (valor <= 0)
				errores.Add("El valor a retirar es invalido");
			return errores;
		}
		public override string Retirar(double valor)
		{
			if (CanWithDraw(valor).Count > 0)
				throw new InvalidOperationException();
			valor = IncluirCostoPorCantidadDeRetiros(valor);
			this.CrearMovimientoDeEgreso(valor);
			return ($"Su Nuevo Saldo es de ${this.Saldo} pesos");
		}
		private double IncluirCostoPorCantidadDeRetiros(double valor)
		{
			int mes = DateTime.Now.Month;
			int anio = DateTime.Now.Year;
			if (this.ObtenerRetirosPorFecha(mes, anio).Count > CANTIDAD_DE_RETIROS_SIN_COSTO)
			{
				valor -= COSTO_POR_RETIRO;
			}
			return valor;
		}
	}


	[Serializable]
	public class CuentaAhorroTopeDeRetiroException : Exception
	{
		public CuentaAhorroTopeDeRetiroException() { }
		public CuentaAhorroTopeDeRetiroException(string message) : base(message) { }
		public CuentaAhorroTopeDeRetiroException(string message, Exception inner) : base(message, inner) { }
		protected CuentaAhorroTopeDeRetiroException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
