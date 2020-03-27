using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class RetirarService
    {
        readonly IUnitOfWork _unitOfWork;
        public RetirarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public RetirarResponse Ejecutar(RetirarRequest request)
        {
            var cuenta = _unitOfWork.CuentaBancariaRepository.
                FindFirstOrDefault(t => t.Numero == request.NumeroCuenta);
            if (cuenta != null)
            {
                cuenta.Retirar(request.Valor);
                _unitOfWork.Commit();
                return new RetirarResponse() { Mensaje = $"Su Nuevo saldo es {cuenta.Saldo}." };
            }
            else
            {
                return new RetirarResponse() { Mensaje = $"Número de Cuenta No Válido." };
            }
        }
    }
    public class RetirarRequest
    {
        public string NumeroCuenta { get; set; }
        public decimal Valor { get; set; }
    }
    public class RetirarResponse
    {
        public string Mensaje { get; set; }
    }
}
