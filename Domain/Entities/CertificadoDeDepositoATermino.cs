using System;
using System.Runtime.Serialization;

namespace Domain.Entities
{
    public class CertificadoDeDepositoATermino : ServicioFinanciero
    {
        private const decimal CONSIGNACION_INICIAL = 1000000;
        public int DiasDeTermino { get; set; }
        public CertificadoDeDepositoATermino()
        {
            this.DiasDeTermino = 30;
        }
        public void ValidarConsignacion(decimal valor)
        {
            if (valor < CONSIGNACION_INICIAL)
            {
                throw new ConsignacionInicialInvalidaException("El valor de la consignacion inicial" +
                    $" debe ser de {CONSIGNACION_INICIAL}");
            }
            else
            {
                this.EjecutarConsignacion(valor);
            }
        }
        public override string Retirar(decimal valor)
        {
            this.ValidarRetiro(valor);
            return "";
        }
        public void ValidarRetiro(decimal valor)
        {
            TimeSpan time = DateTime.Now - this.FechaCreacion;
            int restoDias = time.Days;
            if (restoDias>= DiasDeTermino)
            {
                this.Retirar(valor);   
            }
            else
            {
                throw new RetiroInvalidoPorTiempoTranscurridoException($"No es posible retirar antes de los" +
                    $"{DiasDeTermino} definidos en el contrato");
            }
        }

        public override string Consignar(decimal valor, string ciudadDeOrigen)
        {
            this.ValidarConsignacion(valor);
            return "";
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