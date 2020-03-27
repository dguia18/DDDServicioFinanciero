using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TarjetaDeCredito : ServicioFinanciero
    {
        public decimal CupoPreAprobado { get; set; }
        public void ContratarCupo(decimal valor)
        {
            this.CupoPreAprobado = valor;
            this.Saldo = valor;
        }
        public override string Consignar(decimal valor, string ciudadDeOrigen)
        {
            return ValidarAbonoNegativoOCero(valor);
        }
        private string ValidarAbonoNegativoOCero(decimal valor)
        {
            string respuesta;
            if (valor > 0)
            {
                respuesta = ValidarValorAbonoMaximoAlSaldo(valor);
            }
            else
            {
                respuesta = "El valor a abonar es incorrecto";
            }
            return respuesta;
        }

        private string ValidarValorAbonoMaximoAlSaldo(decimal valor)
        {
            string respuesta;
            if(valor <= this.Saldo)
            {
                respuesta = EjecutarAbono(valor);
            }
            else
            {
                respuesta = $"El valor del abono no puede superar el saldo de: {this.Saldo}";
            }
            return respuesta;
        }
        private string EjecutarAbono(decimal valor)
        {
            this.CupoPreAprobado += valor;
            this.EjecutarConsignacion(valor);
            this.Saldo -= 2 * valor;
            return ($"Su Nuevo Saldo es de ${this.Saldo} pesos");
        }

        public override string Retirar(decimal valor)
        {
            return ValidarAvanceNoNegativoOCero(valor);
        }
        private string ValidarAvanceNoNegativoOCero(decimal valor)
        {
            string respuesta;
            if (valor > 0)
            {
                respuesta = ValidarAvanceNoMayorAlCupo(valor);
            }
            else
            {
                respuesta = "El valor a avanzar es incorrecto";
            }
            return respuesta;
        }
        private string ValidarAvanceNoMayorAlCupo(decimal valor)
        {
            string respuesta;
            if(valor > this.CupoPreAprobado)
            {
                respuesta = $"El valor a avanzar no puede ser mayor al cupo pre-aprobado: {this.CupoPreAprobado}";
            }
            else
            {
                respuesta = EjecutarAvance(valor);
            }
            return respuesta;
        }
        private string EjecutarAvance(decimal valor)
        {
            this.CupoPreAprobado -= valor;
            this.Saldo += 2 * valor;
            this.EjecutarRetiro(valor);
            return ($"Su Nuevo Saldo es de ${this.Saldo} pesos");
        }
    }
}
