using Infrastructure;
using Infrastructure.Base;
using NUnit.Framework;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Application.Test
{
    class ConsignarServiceTest
    {
        BancoContext _context;
        [SetUp]
        public void SetUp()
        {
            var optionsInMemory = new DbContextOptionsBuilder<BancoContext>().UseInMemoryDatabase("Banco").Options;

            _context = new BancoContext(optionsInMemory);
            this.CrearCuentaDeAhorros();
        }
        private void CrearCuentaDeAhorros()
        {
            var request = new CrearServicioFinancieroRequest { Numero = "0001", Nombre = "Duvan Felipe", TipoCuenta = 0, Ciudad = "Valledupar" };
            CrearServicioFinancieroService _service = new CrearServicioFinancieroService(new UnitOfWork(_context));
            _service.Ejecutar(request);
        }
        [TestCaseSource("SourceConsigns")]
        public void ConsignTest(string numeroCuenta, double valor, string ciudadDeConsignacion, string esperado)
        {
            var request = new ConsignarRequest { NumeroCuenta=numeroCuenta,CiudadDeConsignacion=ciudadDeConsignacion, Valor = valor};
            ConsignarService _service = new ConsignarService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            string obtenido = response.Mensaje.Contains(esperado) ? esperado : response.Mensaje;
            Assert.AreEqual(esperado, obtenido);
        }
        private static IEnumerable SourceConsigns()
        {
            yield return new TestCaseData("0001",-500, "Valledupar", "El valor a consignar es incorrecto").SetName("ConsignacionMenorACero");
            yield return new TestCaseData("0001",49950, "Valledupar", "El valor a consignar es incorrecto").SetName("El valor mínimo de la primera consignación debe ser" +
                                $"de ${CuentaAhorro.VALOR_CONSIGNACION_INICIAL} mil pesos.");
            yield return new TestCaseData("0001",50000, "Valledupar", $"Su Nuevo Saldo es de $50000 pesos").SetName("ConsignacionInicialCorrecta");
            yield return new TestCaseData("0001",49950, "Valledupar", $"Su Nuevo Saldo es de ${50000 + 49950} pesos").SetName("ConsignacionPosteriorALaInicialCorrecta");
            yield return new TestCaseData("0001",49950, "Bogota", $"Su Nuevo Saldo es de ${50000 + 39950} pesos").SetName("ConsignacionPosteriorALaInicialCorrectaEnOtraCiudad");
        }

    }
    
}
