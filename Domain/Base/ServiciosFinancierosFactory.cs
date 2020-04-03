using Domain.Contracts;
using Domain.Entities;
using Domain.Enum;
using System;
namespace Domain.Base
{
	public class ServiciosFinancierosFactory : Factory
	{
		public override IServicioFinanciero GetServicioFinanciero(int servicioFinanciero)
		{
			switch (servicioFinanciero)
			{
				case (int)ServicioFinancieroEnum.CUENTA_DE_AHORROS: return new CuentaAhorro();
				case (int)ServicioFinancieroEnum.CUENTA_CORRIENTE: return new CuentaCorriente();
				case (int)ServicioFinancieroEnum.CERTIFICADO_DE_DEPOSITO_A_TERMINO: return new CertificadoDeDepositoATermino();
				case (int)ServicioFinancieroEnum.TARJETA_DE_CREDITO: return new TarjetaDeCredito();
				default: throw new InvalidOperationException();
			}
		}
	}
}
