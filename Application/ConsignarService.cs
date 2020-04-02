using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application
{
	public class ConsignarService
	{
		readonly IUnitOfWork _unitOfWork;

		public ConsignarService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public ConsignarResponse Ejecutar(ConsignarRequest request)
		{
			var cuenta = _unitOfWork.CuentaBancariaRepository.
				FindFirstOrDefault(t => t.Numero == request.NumeroCuenta);
			if (cuenta != null)
			{
				string respuesta;
				IList<string> errores = cuenta.CanConsign(request.Valor);
				if (errores.Count != 0)
				{
					respuesta = String.Join(", ", errores);
				}
				else
				{
					respuesta = cuenta.Consignar(request.Valor, request.CiudadDeConsignacion);
					_unitOfWork.Commit();
				}
				return new ConsignarResponse() { Mensaje = respuesta };
			}
			else
			{
				return new ConsignarResponse() { Mensaje = $"Número de Cuenta No Válido." };
			}
		}
	}
	public class ConsignarRequest
	{
		public string NumeroCuenta { get; set; }
		public double Valor { get; set; }
		public string CiudadDeConsignacion { get; set; }
	}
	public class ConsignarResponse
	{
		public string Mensaje { get; set; }
	}
}
