using Domain.Base;

namespace Domain.Entities
{
    public abstract class Factory
    {
        public abstract IServicioFinanciero GetServicioFinanciero(int servicioFinanciero);
        public static Factory CrearServicioFinancieroFactory(int factoryType)
        {
            return new ServiciosFinancierosFactory();
        }
    }
}
