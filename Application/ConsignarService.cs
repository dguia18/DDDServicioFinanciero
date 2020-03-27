using Domain.Contracts;
using Domain.Entities;

namespace Application
{
    public class ConsignarService 
    {
        readonly IUnitOfWork _unitOfWork;
        
        public  ConsignarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ConsignarResponse Ejecutar(ConsignarRequest request)
        {
            var cuenta = _unitOfWork.CuentaBancariaRepository.
                FindFirstOrDefault(t => t.Numero==request.NumeroCuenta);
            if (cuenta != null)
            {
                string respuesta = cuenta.Consignar(request.Valor,request.CiudadDeConsignacion);
                _unitOfWork.Commit();
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
        public decimal Valor { get; set; }
        public string CiudadDeConsignacion { get; set; }
    }
    public class ConsignarResponse
    {
        public string Mensaje { get; set; }
    }
}
