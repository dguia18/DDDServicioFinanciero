using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Entities
{
    public class CuentaAhorro : ServicioFinanciero
    {
        private bool isConsignacionInicial;
        private const int CANTIDAD_DE_RETIROS_SIN_COSTO = 3;
        private const decimal COSTO_POR_RETIRO = 5000;
        private const int DESCUENTO_POR_SUCURSAL_EN_OTRA_CIUDAD = 10000;
        public const decimal VALOR_CONSIGNACION_INICIAL = 50000;
        public const decimal SALDO_MINIMO = 20000;

        public CuentaAhorro()
        {
            this.isConsignacionInicial = false;
        }

        public void SetIsConsignacionInicial(bool isConsignacionInicial)
        {
            this.isConsignacionInicial = isConsignacionInicial;
        }
        public override IList<string> CanConsign(decimal valor)
        {
            var errores = new List<string>();
            if (this.GetConsignaciones().Count == 0 && valor < VALOR_CONSIGNACION_INICIAL)
                errores.Add("El valor mínimo de la primera consignación debe ser" +
                            $"de ${VALOR_CONSIGNACION_INICIAL} mil pesos.");
            if (valor <= 0)
                errores.Add("El valor a consignar es incorrecto");
            return errores;
        }
                
        public override string Consignar(decimal valor, string ciudadDeOrigen)
        {

            if (CanConsign(valor).Count != 0)
                throw new InvalidOperationException();
            valor = this.IncluirCostoPorCiudadDiferente(valor, ciudadDeOrigen);
            this.CrearMovimientoDeIngreso(valor);
            return ($"Su Nuevo Saldo es de ${this.Saldo} pesos");
        }
        private string IsPrimeraConsignacion(decimal valor, string ciudadDeOrigen)
        {
            string mensaje;
            if (this.isConsignacionInicial)
            {
                mensaje = IsMontoConsignacionInicialValido(valor, ciudadDeOrigen);
            }
            else
            {
                mensaje = VerificarValorNoNegativo(valor, ciudadDeOrigen);
            }

            return mensaje;
        }

        private string IsMontoConsignacionInicialValido(decimal valor, string ciudadDeOrigen)
        {
            string mensaje;
            if (valor >= VALOR_CONSIGNACION_INICIAL)
            {
                this.isConsignacionInicial = true;
                IncluirCostoPorCiudadDiferente(valor, ciudadDeOrigen);
                CrearMovimientoDeIngreso(valor);
                mensaje = $"Su Nuevo Saldo es de ${this.Saldo} pesos";
            }
            else
            {
                mensaje = "El valor mínimo de la primera consignación debe ser" +
                            $"de ${VALOR_CONSIGNACION_INICIAL} mil pesos. " +
                            $"Su nuevo saldo es ${this.Saldo} pesos";
            }

            return mensaje;
        }

        private string VerificarValorNoNegativo(decimal valor, string ciudadDeOrigen)
        {
            string mensaje;
            if (valor > 0)
            {
                valor = this.IncluirCostoPorCiudadDiferente(valor, ciudadDeOrigen);
                this.CrearMovimientoDeIngreso(valor);
                mensaje = $"Su Nuevo Saldo es de ${this.Saldo} pesos";
            }
            else
            {
                mensaje = "El valor a consignar es incorrecto";
            }

            return mensaje;
        }

        private decimal IncluirCostoPorCiudadDiferente(decimal valor, string ciudadDeOrigen)
        {
            return !ciudadDeOrigen.Equals(this.Ciudad) ? valor  - DESCUENTO_POR_SUCURSAL_EN_OTRA_CIUDAD : valor;
        }
        public override IList<string> CanWithDraw(decimal valor)
        {
            var errores = new List<string>();
            decimal nuevoSaldo = Saldo - valor;
            if (nuevoSaldo < SALDO_MINIMO)
                errores.Add($"No es posible realizar el Retiro, el nuevo saldo es menor al minimo, ${SALDO_MINIMO}");        
            if (valor <= 0)
                errores.Add("El valor a retirar es invalido");
            return errores;
        }

        public override string Retirar(decimal valor)
        {
            if (CanWithDraw(valor).Count > 0)
                throw new InvalidOperationException();
            valor = IncluirCostoPorCantidadDeRetiros(valor);
            this.CrearMovimientoDeEgreso(valor);
            return ($"Su Nuevo Saldo es de ${this.Saldo} pesos");
        }

        private string IsMontoNoNegativo(decimal valor)
        {
            string respuesta = "";
            if(valor > 0)
            {
                respuesta = VerificarSaldoMinimo(valor);
            }
            else
            {
                respuesta = "El valor a retirar es invalido";

            }
            return respuesta;
        }

        private string VerificarSaldoMinimo(decimal valor)
        {
            string mensaje;
            decimal nuevoSaldo = Saldo - valor;
            if (nuevoSaldo > SALDO_MINIMO)
            {
                valor = IncluirCostoPorCantidadDeRetiros(valor);
                this.CrearMovimientoDeEgreso(valor);
                mensaje = $"Su Nuevo Saldo es de ${this.Saldo} pesos";
            }
            else
            {
                mensaje = "No es posible realizar el Retiro, el nuevo saldo es menor al minimo";
            }

            return mensaje;
        }

        private decimal IncluirCostoPorCantidadDeRetiros(decimal valor)
        {
            int mes = DateTime.Now.Month;
            int anio = DateTime.Now.Year;
            if (this.ObtenerRetirosPorFecha(mes, anio).Count > CANTIDAD_DE_RETIROS_SIN_COSTO)
            {
                valor -= COSTO_POR_RETIRO;
            }
            return valor;
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
