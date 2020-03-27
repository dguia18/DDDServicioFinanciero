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

            string respuesta = cuentaCorriente.Consignar(-500,"No implementa");

            Assert.AreEqual("El valor a consignar es incorrecto", respuesta);
        }
        [Test]
        public void ValidarValorConsignacionInicialIncorrecto()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";            
            CuentaCorriente cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = numeroDeCuenta;
            cuentaCorriente.Nombre = nombreDeCuenta;            

            string respuesta = cuentaCorriente.Consignar(500,"No implementa");

            Assert.AreEqual("No es posible realizar la consignacion, el monto minimo debe ser de: 10000", respuesta);
        }
        [Test]
        public void ValidarValorConsignacionInicialCorrecto()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";            
            CuentaCorriente cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = numeroDeCuenta;
            cuentaCorriente.Nombre = nombreDeCuenta;            

            string respuesta = cuentaCorriente.Consignar(20000,"No implementa");

            Assert.AreEqual($"Su Nuevo Saldo es de ${cuentaCorriente.Saldo} pesos", respuesta);
        }
        [Test]
        public void ConsignacionCorrectaPosteriorALaInicial()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";            
            CuentaCorriente cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = numeroDeCuenta;
            cuentaCorriente.Nombre = nombreDeCuenta;
            cuentaCorriente.tieneConsignaciones = true;
            string respuesta = cuentaCorriente.Consignar(20000,"No implementa");

            Assert.AreEqual($"Su Nuevo Saldo es de ${cuentaCorriente.Saldo} pesos", respuesta);
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

            string respuesta = cuentaCorriente.Retirar(-500);

            Assert.AreEqual("El valor a retirar es incorrecto", respuesta);
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

            string respuesta = cuentaCorriente.Retirar(15000);

            Assert.AreEqual($"No es posible realizar el retiro, su saldo es menor al cupo " +
                    $"de sobregiro contratado:{cuentaCorriente.CupoDeSobregiro}", respuesta);
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

            string respuesta = cuentaCorriente.Retirar(9000);

            Assert.AreEqual($"Su Nuevo Saldo es de ${cuentaCorriente.Saldo} pesos", respuesta);
        }
    }
}
