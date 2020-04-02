using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Test
{
    public class TarjetaDeCreditoTest
    {
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void ValidarAbonoNegativoOCero()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            TarjetaDeCredito tarjetaDeCredito = new TarjetaDeCredito();
            tarjetaDeCredito.Numero = numeroDeCuenta;
            tarjetaDeCredito.Nombre = nombreDeCuenta;
            tarjetaDeCredito.ContratarCupo(1000000);
            IList<string> errores = tarjetaDeCredito.CanConsign(-500);
            string obtenido;
            string esperado = "El valor a abonar es incorrecto";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = tarjetaDeCredito.Consignar(-500, "No implementa");


            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void AbonoMayorAlSaldo()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            TarjetaDeCredito tarjetaDeCredito = new TarjetaDeCredito();
            tarjetaDeCredito.Numero = numeroDeCuenta;
            tarjetaDeCredito.Nombre = nombreDeCuenta;
            tarjetaDeCredito.ContratarCupo(1000000);
            IList<string> errores = tarjetaDeCredito.CanConsign(5000000);
            string obtenido;
            string esperado = $"El valor del abono no puede superar el saldo de: {tarjetaDeCredito.Saldo}";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = tarjetaDeCredito.Consignar(5000000, "No implementa");


            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void AbonoCorrecto()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            TarjetaDeCredito tarjetaDeCredito = new TarjetaDeCredito();
            tarjetaDeCredito.Numero = numeroDeCuenta;
            tarjetaDeCredito.Nombre = nombreDeCuenta;
            tarjetaDeCredito.ContratarCupo(2000000);
            IList<string> errores = tarjetaDeCredito.CanConsign(1500000);
            string obtenido;
            string esperado = $"Su Nuevo Saldo es de ${500000} pesos";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = tarjetaDeCredito.Consignar(1500000, "No implementa");


            Assert.AreEqual(esperado, obtenido);
        }

        //Retiros

        [Test]
        public void ValidarAvanceNegativoOCero()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            TarjetaDeCredito tarjetaDeCredito = new TarjetaDeCredito();
            tarjetaDeCredito.Numero = numeroDeCuenta;
            tarjetaDeCredito.Nombre = nombreDeCuenta;
            tarjetaDeCredito.ContratarCupo(2000000);
            IList<string> errores = tarjetaDeCredito.CanWithDraw(-500);
            string obtenido;
            string esperado = "El valor a avanzar es incorrecto";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = tarjetaDeCredito.Retirar(-500);


            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void AvanceMayorAlCupo()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            TarjetaDeCredito tarjetaDeCredito = new TarjetaDeCredito();
            tarjetaDeCredito.Numero = numeroDeCuenta;
            tarjetaDeCredito.Nombre = nombreDeCuenta;
            tarjetaDeCredito.ContratarCupo(2000000);
            IList<string> errores = tarjetaDeCredito.CanWithDraw(2500000);
            string obtenido;
            string esperado = $"El valor a avanzar no puede ser mayor al cupo pre-aprobado: " +
                $"{tarjetaDeCredito.CupoPreAprobado}";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = tarjetaDeCredito.Retirar(2500000);


            Assert.AreEqual(esperado, obtenido);            
        }
        
        [Test]
        public void RetiroExitoso()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            TarjetaDeCredito tarjetaDeCredito = new TarjetaDeCredito();
            tarjetaDeCredito.Numero = numeroDeCuenta;
            tarjetaDeCredito.Nombre = nombreDeCuenta;
            tarjetaDeCredito.ContratarCupo(2000000);

            IList<string> errores = tarjetaDeCredito.CanWithDraw(1500000);
            string obtenido;
            string esperado = $"Su Nuevo Saldo es de ${3500000} pesos";
            if (errores.Contains(esperado))
                obtenido = esperado;
            else
                obtenido = tarjetaDeCredito.Retirar(1500000);


            Assert.AreEqual(esperado, obtenido);
        }
    }
}
