using Domain.Contracts;
using Domain.Entities;
using Domain.Enum;

namespace Domain.Base
{
    public class CuentaBancariaFactory : ServicioFinancieroFactory
    {
        public override IServicioFinanciero GetServicioFinanciero(int servicioFinanciero)
        {
            switch (servicioFinanciero)
            {
                case (int)CuentaBancariaEnum.CUENTA_DE_AHORROS: return new CuentaAhorro();
                case (int)CuentaBancariaEnum.CUENTA_CORRIENTE: return new CuentaCorriente();
                case (int)CuentaBancariaEnum.CERTIFICADO_DE_DEPOSITO_A_TERMINO: return new CertificadoDeDepositoATermino();
                default: return new CuentaAhorro();
            }
        }
    }
}
