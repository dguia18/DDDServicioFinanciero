using System;

namespace Domain.Entities
{
    public class CuentaCorriente : ServicioFinanciero
    {
        public decimal CupoDeSobregiro { get; set; }
        private const decimal VALOR_MINIMO_CONSIGNACION_INICIAL = 10000;
        private const decimal CUATRO_X_MIL = 4/1000;
        public bool IsConsignacionInicial { get; set; }
        public CuentaCorriente()
        {
            this.IsConsignacionInicial = false;
        }
        public override string Consignar(decimal valor, string ciudadDeOrigen)
        {            
            return VerificarIsConsignacionInicial(valor);
        }
        private string VerificarIsConsignacionInicial(decimal valor)
        {
            string respuesta;
            if (IsConsignacionInicial)
            {
                respuesta = ValidarValorConsignacioInicial(valor);
            }
            else
            {
                this.EjecutarConsignacion(valor);
                respuesta = $"Su Nuevo Saldo es de ${this.Saldo} pesos";
            }
            return respuesta;
        }
        private string ValidarValorConsignacioInicial(decimal valor)
        {
            string respuesta;
            if(valor > VALOR_MINIMO_CONSIGNACION_INICIAL)
            {
                this.EjecutarConsignacion(valor);
                respuesta = $"Su Nuevo Saldo es de ${this.Saldo} pesos";
            }
            else
            {
                respuesta = $"No es posible realizar la consignacion, el monto minimo debe ser de: {VALOR_MINIMO_CONSIGNACION_INICIAL}";
            }
            return respuesta;
        }
        public override string Retirar(decimal valor)
        {
            return VerificarSaldoMinimo(valor);
        }        
        private string VerificarSaldoMinimo(decimal valor)
        {
            string respuesta;
            decimal nuevoSaldo = this.Saldo - valor;
            if (nuevoSaldo >= CupoDeSobregiro)
            {
                valor = DebitarCuatroXMil(valor);
                this.EjecutarRetiro(valor);
                respuesta = $"Su Nuevo Saldo es de ${this.Saldo} pesos";
            }
            else
            {
                respuesta = $"No es posible realizar el retiro, su saldo es menor al cupo de sobregiro contratado";
            }
            return respuesta;
        }

        private decimal DebitarCuatroXMil(decimal valor)
        {
            return valor * (1 - CUATRO_X_MIL);
        }
    }

    [Serializable]
    public class CuentaCorrienteRetirarMaximoSobregiroException : Exception
    {
        public CuentaCorrienteRetirarMaximoSobregiroException() { }
        public CuentaCorrienteRetirarMaximoSobregiroException(string message) : base(message) { }
        public CuentaCorrienteRetirarMaximoSobregiroException(string message, Exception inner) : base(message, inner) { }
        protected CuentaCorrienteRetirarMaximoSobregiroException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
