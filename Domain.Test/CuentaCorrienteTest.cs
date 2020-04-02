using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Test
{
    class CuentaCorrienteTest
    {
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void ValidarValorNoNegativoConsignacion()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";            
            CuentaCorriente cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = numeroDeCuenta;
            cuentaCorriente.Nombre = nombreDeCuenta;

            IList<string> errores = cuentaCorriente.CanConsign(-500);
            string obtenido;
            string esperado = "El valor a consignar es incorrecto";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cuentaCorriente.Consignar(-500, "No implementa");


            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void ValidarValorConsignacionInicialIncorrecto()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";            
            CuentaCorriente cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = numeroDeCuenta;
            cuentaCorriente.Nombre = nombreDeCuenta;
            IList<string> errores = cuentaCorriente.CanConsign(500);
            string obtenido;
            string esperado = $"No es posible realizar la consignacion, el monto minimo debe ser de: {CuentaCorriente.VALOR_MINIMO_CONSIGNACION_INICIAL}";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cuentaCorriente.Consignar(500, "No implementa");


            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void ValidarValorConsignacionInicialCorrecto()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";            
            CuentaCorriente cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = numeroDeCuenta;
            cuentaCorriente.Nombre = nombreDeCuenta;

            IList<string> errores = cuentaCorriente.CanConsign(20000);
            string obtenido;
            string esperado = $"Su Nuevo Saldo es de $20000 pesos";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cuentaCorriente.Consignar(20000, "No implementa");


            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void ConsignacionCorrectaPosteriorALaInicial()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";            
            CuentaCorriente cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = numeroDeCuenta;
            cuentaCorriente.Nombre = nombreDeCuenta;

            cuentaCorriente.Consignar(20000, "No implementa");
            IList<string> errores = cuentaCorriente.CanConsign(20000);
            string obtenido;
            string esperado = $"Su Nuevo Saldo es de $40000 pesos";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cuentaCorriente.Consignar(20000, "No implementa");


            Assert.AreEqual(esperado, obtenido);
        }

        //Retiros

        [Test]
        public void RetirarConValorNegativo()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";            
            CuentaCorriente cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = numeroDeCuenta;
            cuentaCorriente.Nombre = nombreDeCuenta;
            cuentaCorriente.CupoDeSobregiro = -10000;

            IList<string> errores = cuentaCorriente.CanWithDraw(-500);
            string obtenido;
            string esperado = "El valor a retirar es incorrecto";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cuentaCorriente.Retirar(-500);


            Assert.AreEqual(esperado, obtenido);
        }

        [Test]
        public void VerficarSaldoMayorAlMinimoDespuesDeRetirar()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            CuentaCorriente cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = numeroDeCuenta;
            cuentaCorriente.Nombre = nombreDeCuenta;
            cuentaCorriente.CupoDeSobregiro = -10000;

            IList<string> errores = cuentaCorriente.CanWithDraw(15000);
            string obtenido;
            string esperado = $"No es posible realizar el retiro, su saldo es menor al cupo " +
                    $"de sobregiro contratado:{cuentaCorriente.CupoDeSobregiro}";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cuentaCorriente.Retirar(15000);


            Assert.AreEqual(esperado, obtenido);

        }
        [Test]
        public void RetiroConExito()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            CuentaCorriente cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = numeroDeCuenta;
            cuentaCorriente.Nombre = nombreDeCuenta;
            cuentaCorriente.CupoDeSobregiro = -10000;

            IList<string> errores = cuentaCorriente.CanWithDraw(9000);
            string obtenido;
            string esperado = $"Su Nuevo Saldo es de ${-9000} pesos";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cuentaCorriente.Retirar(9000);

            Assert.AreEqual(esperado, obtenido);
        }
    }
}
