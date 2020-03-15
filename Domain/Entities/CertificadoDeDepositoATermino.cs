using System;
using System.Runtime.Serialization;

namespace Domain.Entities
{
    public class CertificadoDeDepositoATermino : CuentaBancaria
    {
        private const decimal CONSIGNACION_INICIAL = 1000000;
        public int PlazoDefinidoEnDias { get; set; }

        public void ValidarConsignacion(decimal valor)
        {
            if (valor < CONSIGNACION_INICIAL)
            {
                throw new ConsignacionInicialInvalidaException("El valor de la consignacion inicial" +
                    " debe ser de 1000000");
            }
            else
            {
                this.Consignar(valor);
            }
        }
        public void ValidarRetirar(decimal valor, int diasTrasncurridos)
        {
            if (PlazoDefinidoEnDias <= diasTrasncurridos)
            {
                this.Retirar(valor);   
            }
            else
            {
                throw new RetiroInvalidoPorTiempoTranscurridoException($"No es posible retirar antes de los" +
                    $"{PlazoDefinidoEnDias} definidos en el contrato");
            }
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