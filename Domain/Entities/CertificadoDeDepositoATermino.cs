using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Entities
{
    public class CertificadoDeDepositoATermino : ServicioFinanciero
    {
        public const decimal VALOR_CONSIGNACION_INICIAL = 1000000;
        public bool TieneConsignacion { get; set; }
        public int DiasDeTermino { get; set; }
        public CertificadoDeDepositoATermino()
        {
            this.DiasDeTermino = 30;
        }
        public override IList<string> CanConsign(decimal valor)
        {
            var errores = new List<string>();
            TimeSpan time = DateTime.Now - this.FechaCreacion;
            int restoDias = time.Days;
            if (this.GetConsignaciones().Count == 0 && valor < VALOR_CONSIGNACION_INICIAL)
                errores.Add("El valor de la consignacion inicial" +
                    $" debe ser de {VALOR_CONSIGNACION_INICIAL}");
            if (this.GetConsignaciones().Count > 0 && restoDias <= DiasDeTermino)
                errores.Add("No es posible realizar una segunda consignacion");
            if (valor <= 0)
                errores.Add("El valor a consignar es incorrecto");
            return errores;
        }


        public override string Consignar(decimal valor, string ciudadDeOrigen)
        {
            if (CanConsign(valor).Count > 0)
                throw new InvalidOperationException();
            this.EjecutarConsignacion(valor);
            return ($"Su Nuevo Saldo es de ${this.Saldo} pesos");
        }
        private string ValidarMontoNoNegativoDeConsignacion(decimal valor)
        {
            string respuesta;
            if (valor > 0)
            {
                respuesta = ValidarUnicaConsignacion(valor);
            }
            else
            {
                respuesta = "El valor a consignar es incorrecto";
            }
            return respuesta;
        }
        private string ValidarUnicaConsignacion(decimal valor)
        {
            string respuesta;
            if (!TieneConsignacion)
            {
                respuesta = ValidarValorConsignacion(valor);
            }
            else
            {
                respuesta = "No es posible realizar una segunda consignacion";
            }
            return respuesta;
        }
        public string ValidarValorConsignacion(decimal valor)
        {
            string respuesta;
            if (valor < VALOR_CONSIGNACION_INICIAL)
            {
                respuesta = "El valor de la consignacion inicial" +
                    $" debe ser de {VALOR_CONSIGNACION_INICIAL}";
            }
            else
            {
                this.EjecutarConsignacion(valor);
                this.TieneConsignacion = true;
                respuesta = $"Su Nuevo Saldo es de ${this.Saldo} pesos";
            }
            return respuesta;
        }
        public override IList<string> CanWithDraw(decimal valor)
        {
            var errores = new List<string>();
            TimeSpan time = DateTime.Now - this.FechaCreacion;
            int restoDias = time.Days;
            if (restoDias <= DiasDeTermino)
                errores.Add($"No es posible retirar antes de los" +
                    $"{DiasDeTermino} definidos en el contrato");
            if (valor <= 0)
                errores.Add("El valor a retirar es incorrecto");
            decimal nuevoSaldo = this.Saldo - valor;
            if (nuevoSaldo < 0)
                errores.Add($"No es posible realizar el retiro por falta de saldo, su saldo: {this.Saldo}");

            return errores;
        }
        public override string Retirar(decimal valor)
        {
            if (CanWithDraw(valor).Count > 0)
                throw new InvalidOperationException();
            this.EjecutarRetiro(valor);
            return ($"Su Nuevo Saldo es de ${this.Saldo} pesos");
        }

        private string ValidarValorNoNegativoRetiro(decimal valor)
        {
            string respuesta;
            if (valor > 0)
            {
                respuesta = VerificarDiasDeTerminoParaRetiro(valor);
            }
            else
            {
                respuesta = "El valor a retirar es incorrecto";
            }
            return respuesta;
        }
        public string VerificarDiasDeTerminoParaRetiro(decimal valor)
        {
            string respuesta;
            TimeSpan time = DateTime.Now - this.FechaCreacion;
            int restoDias = time.Days;
            if (restoDias >= DiasDeTermino)
            {
                respuesta = ValidarSaldoMinimo(valor);
            }
            else
            {
                respuesta = $"No es posible retirar antes de los" +
                    $"{DiasDeTermino} definidos en el contrato";
            }
            return respuesta;
        }
        private string ValidarSaldoMinimo(decimal valor)
        {
            string respuesta;
            decimal nuevoSaldo = this.Saldo - valor;
            if (nuevoSaldo >= 0)
            {
                this.EjecutarRetiro(valor);
                respuesta = $"Su Nuevo Saldo es de ${this.Saldo} pesos";
            }
            else
            {
                respuesta = $"No es posible realizar el retiro por falta de saldo, su saldo: {this.Saldo}";
            }
            return respuesta;
        }


    }
    [Serializable]
    public class ConsignacionInicialInvalidaException : Exception
    {
        public ConsignacionInicialInvalidaException() : base()
        { }

        public ConsignacionInicialInvalidaException(string message) : base(message)
        { }

        public ConsignacionInicialInvalidaException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public ConsignacionInicialInvalidaException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
    [Serializable]
    public class RetiroInvalidoPorTiempoTranscurridoException : Exception
    {
        public RetiroInvalidoPorTiempoTranscurridoException() : base()
        { }

        public RetiroInvalidoPorTiempoTranscurridoException(string message) : base(message)
        { }

        public RetiroInvalidoPorTiempoTranscurridoException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public RetiroInvalidoPorTiempoTranscurridoException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}