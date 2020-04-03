using Domain.Base;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enum;
using System;

namespace Application
{
	public class CrearServicioFinancieroService
	{
		readonly IUnitOfWork _unitOfWork;
		readonly Factory servicioFinancieroFactory;

		public CrearServicioFinancieroService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			servicioFinancieroFactory = Factory.CrearServicioFinancieroFactory(0);
		}
		public CrearServicioFinancieroResponse Ejecutar(CrearServicioFinancieroRequest request)
		{
			ServicioFinanciero cuenta = _unitOfWork.CuentaBancariaRepository.FindFirstOrDefault(t => t.Numero == request.Numero);
			if (cuenta == null)
			{
				try
				{
					ServicioFinanciero cuentaNueva = (ServicioFinanciero)servicioFinancieroFactory.GetServicioFinanciero(request.TipoCuenta);//Debe ir un factory que determine que tipo de cuenta se va a crear
					cuentaNueva = InicializarServicioFinanciero(cuentaNueva, request);
					_unitOfWork.CuentaBancariaRepository.Add(cuentaNueva);
					_unitOfWork.Commit();
					return new CrearServicioFinancieroResponse() { Mensaje = $"Se creó con exito la cuenta {cuentaNueva.Numero}." };
				}
				catch (System.Exception)
				{
					return new CrearServicioFinancieroResponse() { Mensaje = $"El tipo de servicio eligido, no existe" };

				}
			}
			else
			{
				return new CrearServicioFinancieroResponse() { Mensaje = $"El número de cuenta ya exite" };
			}
		}

		private ServicioFinanciero InicializarServicioFinanciero(ServicioFinanciero servicioFinanciero, CrearServicioFinancieroRequest request)
		{
			servicioFinanciero.Nombre = request.Nombre;
			servicioFinanciero.Numero = request.Numero;
			servicioFinanciero.Ciudad = request.Ciudad;
			switch (request.TipoCuenta)
			{
				case (int)ServicioFinancieroEnum.CUENTA_DE_AHORROS: return servicioFinanciero;
				case (int)ServicioFinancieroEnum.CUENTA_CORRIENTE:
					CuentaCorriente cuentaCorriente = (CuentaCorriente)servicioFinanciero;
					cuentaCorriente.CupoDeSobregiro = request.CupoDeSobregiro;
					return cuentaCorriente;
				case (int)ServicioFinancieroEnum.CERTIFICADO_DE_DEPOSITO_A_TERMINO:
					CertificadoDeDepositoATermino cdt = (CertificadoDeDepositoATermino)servicioFinanciero;
					cdt.DiasDeTermino = request.DiasDeTermino;
					return cdt;
				case (int)ServicioFinancieroEnum.TARJETA_DE_CREDITO:
					TarjetaDeCredito tarjetaDeCredito = (TarjetaDeCredito)servicioFinanciero;
					tarjetaDeCredito.ContratarCupo(request.CupoPreAprobado);
					return tarjetaDeCredito;
				default: throw new InvalidOperationException();
			}
		}


	}
	public class CrearServicioFinancieroRequest
	{
		public string Nombre { get; set; }
		public int TipoCuenta { get; set; }
		public string Numero { get; set; }
		public string Ciudad { get; set; }
		public double CupoDeSobregiro { get; set; }
		public int DiasDeTermino { get; set; }
		public double CupoPreAprobado { get; set; }
	}
	public class CrearServicioFinancieroResponse
	{
		public string Mensaje { get; set; }
	}
}
