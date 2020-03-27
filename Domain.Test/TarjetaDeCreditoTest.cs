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
            string respuesta = tarjetaDeCredito.Consignar(-500, "No implementa");

            Assert.AreEqual("El valor a abonar es incorrecto", respuesta);
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
            string respuesta = tarjetaDeCredito.Consignar(5000000, "No implementa");

            Assert.AreEqual($"El valor del abono no puede superar el saldo de: {tarjetaDeCredito.Saldo}", respuesta);
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
            string respuesta = tarjetaDeCredito.Consignar(1500000, "No implementa");

            Assert.AreEqual($"Su Nuevo Saldo es de ${tarjetaDeCredito.Saldo} pesos", respuesta);
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
            string respuesta = tarjetaDeCredito.Retirar(-500);

            Assert.AreEqual("El valor a avanzar es incorrecto", respuesta);
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
            string respuesta = tarjetaDeCredito.Retirar(2500000);

            Assert.AreEqual($"El valor a avanzar no puede ser mayor al cupo pre-aprobado: " +
                $"{tarjetaDeCredito.CupoPreAprobado}", respuesta);
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

            
            string respuesta = tarjetaDeCredito.Retirar(1500000); 

            Assert.AreEqual($"Su Nuevo Saldo es de ${tarjetaDeCredito.Saldo} pesos", respuesta);
        }
    }
}
