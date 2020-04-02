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

            IList<string> errores = cdt.CanConsign(-500);
            string obtenido;
            string esperado = "El valor a consignar es incorrecto";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cdt.Consignar(-500, "No implementa");


            Assert.AreEqual(esperado, obtenido);

        }
        [Test]
        public void ConsignacionInicialIncorrecta()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            CertificadoDeDepositoATermino cdt = new CertificadoDeDepositoATermino();
            cdt.Numero = numeroDeCuenta;
            cdt.Nombre = nombreDeCuenta;

            IList<string> errores = cdt.CanConsign(500);
            string obtenido;
            string esperado = "El valor de la consignacion inicial" +
                    $" debe ser de {CertificadoDeDepositoATermino.VALOR_CONSIGNACION_INICIAL}";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cdt.Consignar(500, "No implementa");


            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void ConsignacionInicialCorrecta()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            CertificadoDeDepositoATermino cdt = new CertificadoDeDepositoATermino();
            cdt.Numero = numeroDeCuenta;
            cdt.Nombre = nombreDeCuenta;

            IList<string> errores = cdt.CanConsign(1500000);
            string obtenido;
            string esperado = $"Su Nuevo Saldo es de ${1500000} pesos";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cdt.Consignar(1500000, "No implementa");


            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void DobleConsignacion()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            CertificadoDeDepositoATermino cdt = new CertificadoDeDepositoATermino();
            cdt.Numero = numeroDeCuenta;
            cdt.Nombre = nombreDeCuenta;

            cdt.Consignar(1500000, "No implementa");
            IList<string> errores = cdt.CanConsign(1500000);
            string obtenido;
            string esperado = "No es posible realizar una segunda consignacion";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cdt.Consignar(1500000, "No implementa");


            Assert.AreEqual(esperado, obtenido);

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

            IList<string> errores = cdt.CanWithDraw(-500);
            string obtenido;
            string esperado = "El valor a retirar es incorrecto";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cdt.Retirar(-500);

            Assert.AreEqual(esperado, obtenido);

        }
        [Test]
        public void RetiroAntesDeLosDiasDeTermino()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            CertificadoDeDepositoATermino cdt = new CertificadoDeDepositoATermino();
            cdt.Numero = numeroDeCuenta;
            cdt.Nombre = nombreDeCuenta;

            IList<string> errores = cdt.CanWithDraw(500);
            string obtenido;
            string esperado = $"No es posible retirar antes de los" +
                    $"{cdt.DiasDeTermino} definidos en el contrato";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cdt.Retirar(500);

            Assert.AreEqual(esperado, obtenido);
            
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
            IList<string> errores = cdt.CanWithDraw(500);
            string obtenido;
            string esperado = $"No es posible realizar el retiro por falta de saldo, su saldo: {cdt.Saldo}";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cdt.Retirar(500);

            Assert.AreEqual(esperado, obtenido);

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
            IList<string> errores = cdt.CanWithDraw(500);
            string obtenido;
            string esperado = $"Su Nuevo Saldo es de ${1500000-500} pesos";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = cdt.Retirar(500);

            Assert.AreEqual(esperado, obtenido);

        }        
    }
}
