using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Test
{
    public class CertificadoDeDepositoATerminoTest
    {
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void ValidarConsignacionNegativa()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            CertificadoDeDepositoATermino cdt = new CertificadoDeDepositoATermino();
            cdt.Numero = numeroDeCuenta;
            cdt.Nombre = nombreDeCuenta;

            string respuesta = cdt.Consignar(-500, "No implementa");

            Assert.AreEqual("El valor a consignar es incorrecto", respuesta);
        }
        [Test]
        public void ConsignacionInicialIncorrecta()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            CertificadoDeDepositoATermino cdt = new CertificadoDeDepositoATermino();
            cdt.Numero = numeroDeCuenta;
            cdt.Nombre = nombreDeCuenta;

            string respuesta = cdt.Consignar(500, "No implementa");

            Assert.AreEqual("El valor de la consignacion inicial" +
                    $" debe ser de {CertificadoDeDepositoATermino.VALOR_CONSIGNACION_INICIAL}", respuesta);
        }
        [Test]
        public void ConsignacionInicialCorrecta()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            CertificadoDeDepositoATermino cdt = new CertificadoDeDepositoATermino();
            cdt.Numero = numeroDeCuenta;
            cdt.Nombre = nombreDeCuenta;

            string respuesta = cdt.Consignar(1500000, "No implementa");

            Assert.AreEqual($"Su Nuevo Saldo es de ${cdt.Saldo} pesos", respuesta);
        }
        [Test]
        public void DobleConsignacion()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            CertificadoDeDepositoATermino cdt = new CertificadoDeDepositoATermino();
            cdt.Numero = numeroDeCuenta;
            cdt.Nombre = nombreDeCuenta;

            string respuesta = cdt.Consignar(1500000, "No implementa");
            respuesta = cdt.Consignar(1500000, "No implementa");

            Assert.AreEqual("No es posible realizar una segunda consignacion", respuesta);
        }

        //Retiros

        [Test]
        public void ValidarRetiroNegativo()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            CertificadoDeDepositoATermino cdt = new CertificadoDeDepositoATermino();
            cdt.Numero = numeroDeCuenta;
            cdt.Nombre = nombreDeCuenta;

            string respuesta = cdt.Retirar(-500);

            Assert.AreEqual("El valor a retirar es incorrecto", respuesta);
        }
        [Test]
        public void RetiroAntesDeLosDiasDeTermino()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            CertificadoDeDepositoATermino cdt = new CertificadoDeDepositoATermino();
            cdt.Numero = numeroDeCuenta;
            cdt.Nombre = nombreDeCuenta;

            string respuesta = cdt.Retirar(500);

            Assert.AreEqual($"No es posible retirar antes de los" +
                    $"{cdt.DiasDeTermino} definidos en el contrato", respuesta);
        }
        [Test]
        public void RetiroDejandoSaldoNegativo()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            CertificadoDeDepositoATermino cdt = new CertificadoDeDepositoATermino();
            cdt.Numero = numeroDeCuenta;
            cdt.Nombre = nombreDeCuenta;
            cdt.DiasDeTermino = -1;
            string respuesta = cdt.Retirar(500);

            Assert.AreEqual($"No es posible realizar el retiro por falta de saldo, su saldo: {cdt.Saldo}", respuesta);
        }  
        [Test]
        public void RetiroExitoso()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            CertificadoDeDepositoATermino cdt = new CertificadoDeDepositoATermino();
            cdt.Numero = numeroDeCuenta;
            cdt.Nombre = nombreDeCuenta;
            cdt.DiasDeTermino = -1;//Con tal de validarlo
            cdt.Consignar(1500000, "No implementa");
            string respuesta = cdt.Retirar(500);

            Assert.AreEqual($"Su Nuevo Saldo es de ${cdt.Saldo} pesos", respuesta);
        }        
    }
}
