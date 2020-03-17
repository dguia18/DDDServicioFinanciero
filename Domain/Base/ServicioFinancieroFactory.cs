using Domain.Base;

namespace Domain.Entities
{
    public abstract class ServicioFinancieroFactory
    {
        public abstract IServicioFinanciero GetServicioFinanciero(int servicioFinanciero);
        public static ServicioFinancieroFactory CrearServicioFinancieroFactory(int factoryType)
        {
            return new CuentaBancariaFactory();
        }
    }
}
