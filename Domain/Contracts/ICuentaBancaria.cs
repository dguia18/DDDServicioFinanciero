using Domain.Entities;

namespace Domain.Contracts
{
    public interface ICuentaBancaria
    {
        IServicioFinanciero CrearCuentaBancaria();
    }
}
