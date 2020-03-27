using System;

namespace Domain.Entities
{
    public class CuentaCorriente : ServicioFinanciero
    {
        public decimal CupoDeSobregiro { get; set; }
        private const decimal VALOR_MINIMO_CONSIGNACION_INICIAL = 10000;
        private const decimal CUATRO_X_MIL = 4/1000;
        public bool tieneConsignaciones { get; set; }
        public CuentaCorriente()
        {
            this.tieneConsignaciones = false;
        }
        public override string Consignar(decimal valor, string ciudadDeOrigen)
        {            
            return ValidarValorNoNegativoAConsignar(valor);
        }
        private string ValidarValorNoNegativoAConsignar(decimal valor)
        {
            string respuesta;
            if (valor > 0)
            {
                respuesta = VerificarIsConsignacionInicial(valor);
            }
            else
            {
                respuesta = "El valor a consignar es incorrecto";
            }
            return respuesta;
        }
        private string VerificarIsConsignacionInicial(decimal valor)
        {
            string respuesta;
            if (tieneConsignaciones)
            {
                this.EjecutarConsignacion(valor);
                respuesta = $"Su Nuevo Saldo es de ${this.Saldo} pesos";
            }
            else
            {
                respuesta = ValidarValorConsignacioInicial(valor);
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
                this.tieneConsignaciones = true;
            }
            else
            {
                respuesta = $"No es posible realizar la consignacion, el monto minimo debe ser de: {VALOR_MINIMO_CONSIGNACION_INICIAL}";
            }
            return respuesta;
        }
        public override string Retirar(decimal valor)
        {
            return ValidarValorNoNegativo(valor);
        }   
        private string ValidarValorNoNegativo(decimal valor)
        {
            string respuesta;
            if(valor > 0)
            {
                respuesta = VerificarSaldoMinimo(valor);
            }
            else
            {
                respuesta = "El valor a retirar es incorrecto";
            }
            return respuesta;
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
                respuesta = $"No es posible realizar el retiro, su saldo es menor al cupo " +
                    $"de sobregiro contratado:{this.CupoDeSobregiro}";
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
