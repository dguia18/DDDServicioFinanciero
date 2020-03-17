using System;

namespace Domain.Entities
{
    public class CuentaAhorro : CuentaBancaria
    {
        public const decimal TOPERETIRO = 1000;

        public override void Retirar(decimal valor)
        {
            this.ValidarRetiro(valor);
        }

        public void ValidarRetiro(decimal valor)
        {
            decimal nuevoSaldo = Saldo - valor;
            if (nuevoSaldo > TOPERETIRO)
            {
                this.EjecutarRetiro(valor);
            }
            else
            {
                throw new CuentaAhorroTopeDeRetiroException("No es posible realizar el Retiro, Supera el tope mínimo permitido de retiro");
            }
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
