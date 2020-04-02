using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Domain.Test
{
    public class CuentaAhorroTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConsignacionMenorACero()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            string ciudad = "Valledupar";
            ServicioFinanciero cuentaDeAhorro = new CuentaAhorro();
            cuentaDeAhorro.Numero = numeroDeCuenta;
            cuentaDeAhorro.Nombre = nombreDeCuenta;
            cuentaDeAhorro.Ciudad = ciudad;
            IList<string> errores = cuentaDeAhorro.CanConsign(-500);
            string obtenido = "";
            string esperado = "El valor a consignar es incorrecto";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cuentaDeAhorro.Consignar(-500, "Valledupar");


            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void ConsignacionInicialCorrecta()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            string ciudad = "Valledupar";
            CuentaAhorro cuentaDeAhorro = new CuentaAhorro();
            cuentaDeAhorro.Numero = numeroDeCuenta;
            cuentaDeAhorro.Nombre = nombreDeCuenta;
            cuentaDeAhorro.Ciudad = ciudad;
            IList<string> errores = cuentaDeAhorro.CanConsign(50000);
            string obtenido;
            string esperado = $"Su Nuevo Saldo es de $50000 pesos";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cuentaDeAhorro.Consignar(50000, "Valledupar");

            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void ConsignacionInicialInCorrecta()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            string ciudad = "Valledupar";
            CuentaAhorro cuentaDeAhorro = new CuentaAhorro();
            cuentaDeAhorro.Numero = numeroDeCuenta;
            cuentaDeAhorro.Nombre = nombreDeCuenta;
            cuentaDeAhorro.Ciudad = ciudad;
            IList<string> errores = cuentaDeAhorro.CanConsign(49950);
            string obtenido;
            string esperado = "El valor mínimo de la primera consignación debe ser" +
                                $"de ${CuentaAhorro.VALOR_CONSIGNACION_INICIAL} mil pesos.";
                                
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cuentaDeAhorro.Consignar(49950, "Valledupar");
            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void ConsignacionPosteriorALaInicialCorrecta()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            string ciudad = "Valledupar";
            CuentaAhorro cuentaDeAhorro = new CuentaAhorro();
            cuentaDeAhorro.Numero = numeroDeCuenta;
            cuentaDeAhorro.Nombre = nombreDeCuenta;
            cuentaDeAhorro.Ciudad = ciudad;
            cuentaDeAhorro.Consignar(50000,"Valledupar");

            IList<string> errores = cuentaDeAhorro.CanConsign(49950);
            string obtenido;
            string esperado = $"Su Nuevo Saldo es de ${50000+49950} pesos";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cuentaDeAhorro.Consignar(49950, "Valledupar");            

            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void ConsignacionPosteriorALaInicialCorrectaEnOtraCiudad()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            string ciudad = "Bogota";
            CuentaAhorro cuentaDeAhorro = new CuentaAhorro();
            cuentaDeAhorro.Numero = numeroDeCuenta;
            cuentaDeAhorro.Nombre = nombreDeCuenta;
            cuentaDeAhorro.Ciudad = ciudad;
            cuentaDeAhorro.Consignar(50000, "Bogota");

            IList<string> errores = cuentaDeAhorro.CanConsign(50000);
            string obtenido;
            string esperado = $"Su Nuevo Saldo es de ${50000 + 39950} pesos";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cuentaDeAhorro.Consignar(49950, "Valledupar");

            Assert.AreEqual(esperado, obtenido);
        }

        //Retiros

        [Test]
        public void RetiroMenorACero()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            string ciudad = "Valledupar";
            ServicioFinanciero cuentaDeAhorro = new CuentaAhorro();
            cuentaDeAhorro.Numero = numeroDeCuenta;
            cuentaDeAhorro.Nombre = nombreDeCuenta;
            cuentaDeAhorro.Ciudad = ciudad;

            IList<string> errores = cuentaDeAhorro.CanWithDraw(-500);
            string obtenido;
            string esperado = "El valor a retirar es invalido";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cuentaDeAhorro.Retirar(-500);

            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void SaldoMenorAlMinimo()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            string ciudad = "Valledupar";
            ServicioFinanciero cuentaDeAhorro = new CuentaAhorro();
            cuentaDeAhorro.Numero = numeroDeCuenta;
            cuentaDeAhorro.Nombre = nombreDeCuenta;
            cuentaDeAhorro.Ciudad = ciudad;

            IList<string> errores = cuentaDeAhorro.CanWithDraw(500);
            string obtenido;
            string esperado = $"No es posible realizar el Retiro, el nuevo saldo es menor al minimo, ${CuentaAhorro.SALDO_MINIMO}";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cuentaDeAhorro.Retirar(500);

            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void RetiroConExito()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            string ciudad = "Valledupar";
            ServicioFinanciero cuentaDeAhorro = new CuentaAhorro();
            cuentaDeAhorro.Numero = numeroDeCuenta;
            cuentaDeAhorro.Nombre = nombreDeCuenta;
            cuentaDeAhorro.Ciudad = ciudad;
            cuentaDeAhorro.Saldo = 30000;

            IList<string> errores = cuentaDeAhorro.CanWithDraw(500);
            string obtenido;
            string esperado = $"Su Nuevo Saldo es de ${29500} pesos";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cuentaDeAhorro.Retirar(500);

            Assert.AreEqual(esperado, obtenido);
        }

    }
}