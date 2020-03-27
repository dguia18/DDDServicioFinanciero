using Domain.Entities;
using NUnit.Framework;

namespace Domain.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AbonoMenorACero()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            string ciudad = "Valledupar";
            ServicioFinanciero servicios = new CuentaAhorro();
            servicios.Numero = numeroDeCuenta;
            servicios.Nombre = nombreDeCuenta;
            servicios.Ciudad = ciudad;

            string respuesta = servicios.Consignar(-500, "Valledupar");

            Assert.AreEqual("El valor a consignar es incorrecto", respuesta);
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
            cuentaDeAhorro.SetIsConsignacionInicial(true);
            string respuesta = cuentaDeAhorro.Consignar(50000, "Valledupar");

            Assert.AreEqual($"Su Nuevo Saldo es de ${cuentaDeAhorro.Saldo} pesos", respuesta);
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
            cuentaDeAhorro.SetIsConsignacionInicial(true);
            string respuesta = cuentaDeAhorro.Consignar(49950, "Valledupar");

            Assert.AreEqual("El valor mínimo de la primera consignación debe ser" +
                                $"de ${50000} mil pesos. " +
                                $"Su nuevo saldo es ${cuentaDeAhorro.Saldo} pesos", respuesta);
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
            cuentaDeAhorro.Saldo =30000;
            string respuesta = cuentaDeAhorro.Consignar(49950, "Valledupar");

            Assert.AreEqual($"Su Nuevo Saldo es de ${cuentaDeAhorro.Saldo} pesos", respuesta);
        }
        [Test]
        public void ConsignacionPosteriorALaInicialInCorrecta()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            string ciudad = "Bogota";
            CuentaAhorro cuentaDeAhorro = new CuentaAhorro();
            cuentaDeAhorro.Numero = numeroDeCuenta;
            cuentaDeAhorro.Nombre = nombreDeCuenta;
            cuentaDeAhorro.Ciudad = ciudad;
            cuentaDeAhorro.Saldo = 30000;
            string respuesta = cuentaDeAhorro.Consignar(49950, "Valledupar");

            Assert.AreEqual($"Su Nuevo Saldo es de ${cuentaDeAhorro.Saldo} pesos", respuesta);
        }

        //Retiros

        [Test]
        public void RetiroMenorACero()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            string ciudad = "Valledupar";
            ServicioFinanciero servicios = new CuentaAhorro();
            servicios.Numero = numeroDeCuenta;
            servicios.Nombre = nombreDeCuenta;
            servicios.Ciudad = ciudad;

            string respuesta = servicios.Retirar(-500);

            Assert.AreEqual("El valor a retirar es invalido", respuesta);
        }
        [Test]
        public void SaldoMenorAlMinimo()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            string ciudad = "Valledupar";
            ServicioFinanciero servicios = new CuentaAhorro();
            servicios.Numero = numeroDeCuenta;
            servicios.Nombre = nombreDeCuenta;
            servicios.Ciudad = ciudad;

            string respuesta = servicios.Retirar(500);

            Assert.AreEqual("No es posible realizar el Retiro, el nuevo saldo es menor al minimo", respuesta);
        }
        [Test]
        public void RetiroConExito()
        {
            string numeroDeCuenta = "1001";
            string nombreDeCuenta = "Cuenta de Ejemplo";
            string ciudad = "Valledupar";
            ServicioFinanciero servicios = new CuentaAhorro();
            servicios.Numero = numeroDeCuenta;
            servicios.Nombre = nombreDeCuenta;
            servicios.Ciudad = ciudad;
            servicios.Saldo = 30000;

            string respuesta = servicios.Retirar(500);

            Assert.AreEqual($"Su Nuevo Saldo es de ${servicios.Saldo} pesos", respuesta);
        }

    }
}