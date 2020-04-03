using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Application.Test
{
	public class Tests
	{
		BancoContext _context;

		[SetUp]
		public void Setup()
		{
			/*var optionsSqlServer = new DbContextOptionsBuilder<BancoContext>()
             .UseSqlServer("Server=.\\;Database=Banco;Trusted_Connection=True;MultipleActiveResultSets=true")
             .Options;*/

			var optionsInMemory = new DbContextOptionsBuilder<BancoContext>().UseInMemoryDatabase("Banco").Options;

			_context = new BancoContext(optionsInMemory);
		}

		[Test]
		public void CrearCuentaDeAhorros()
		{
			var request = new CrearServicioFinancieroRequest { Numero = "0001", Nombre = "Duvan Felipe", TipoCuenta = 0, Ciudad = "Valledupar" };
			CrearServicioFinancieroService _service = new CrearServicioFinancieroService(new UnitOfWork(_context));
			var response = _service.Ejecutar(request);
			Assert.AreEqual("Se creó con exito la cuenta 0001.", response.Mensaje);
		}
		[Test]
		public void CrearCuentaDeAhorrosRepetida()
		{
			var request = new CrearServicioFinancieroRequest { Numero = "0001", Nombre = "Duvan Felipe", TipoCuenta = 0, Ciudad = "Valledupar" };
			CrearServicioFinancieroService _service = new CrearServicioFinancieroService(new UnitOfWork(_context));
			var response = _service.Ejecutar(request);
			Assert.AreEqual($"El número de cuenta ya exite", response.Mensaje);
		}
		[Test]
		public void CrearCuentaCorriente()
		{
			var request = new CrearServicioFinancieroRequest { Numero = "0002", Nombre = "aaaaa", TipoCuenta = 1, Ciudad = "Valledupar", CupoDeSobregiro = -10000 };
			CrearServicioFinancieroService _service = new CrearServicioFinancieroService(new UnitOfWork(_context));
			var response = _service.Ejecutar(request);
			Assert.AreEqual("Se creó con exito la cuenta 0002.", response.Mensaje);
		}
		[Test]
		public void CrearCuentaCorrienteRepetida()
		{
			var request = new CrearServicioFinancieroRequest { Numero = "0002", Nombre = "aaaaa", TipoCuenta = 1, Ciudad = "Valledupar", CupoDeSobregiro = -10000 };
			CrearServicioFinancieroService _service = new CrearServicioFinancieroService(new UnitOfWork(_context));
			var response = _service.Ejecutar(request);
			Assert.AreEqual($"El número de cuenta ya exite", response.Mensaje);
		}
		[Test]
		public void CrearCDT()
		{
			var request = new CrearServicioFinancieroRequest { Numero = "0003", Nombre = "aaaaa", TipoCuenta = 2, Ciudad = "Valledupar", DiasDeTermino = 90 };
			CrearServicioFinancieroService _service = new CrearServicioFinancieroService(new UnitOfWork(_context));
			var response = _service.Ejecutar(request);
			Assert.AreEqual("Se creó con exito la cuenta 0003.", response.Mensaje);
		}
		[Test]
		public void CrearCDTRepetido()
		{
			var request = new CrearServicioFinancieroRequest { Numero = "0003", Nombre = "aaaaa", TipoCuenta = 2, Ciudad = "Valledupar", DiasDeTermino = 60 };
			CrearServicioFinancieroService _service = new CrearServicioFinancieroService(new UnitOfWork(_context));
			var response = _service.Ejecutar(request);
			Assert.AreEqual($"El número de cuenta ya exite", response.Mensaje);
		}
		[Test]
		public void CrearTarjetaDeCredito()
		{
			var request = new CrearServicioFinancieroRequest { Numero = "0004", Nombre = "aaaaa", TipoCuenta = 3, Ciudad = "Valledupar", CupoPreAprobado = 1000000 };
			CrearServicioFinancieroService _service = new CrearServicioFinancieroService(new UnitOfWork(_context));
			var response = _service.Ejecutar(request);
			Assert.AreEqual("Se creó con exito la cuenta 0004.", response.Mensaje);
		}
		[Test]
		public void CrearTarjetaDeCreditoRepetida()
		{
			var request = new CrearServicioFinancieroRequest { Numero = "0004", Nombre = "aaaaa", TipoCuenta = 3, Ciudad = "Valledupar", CupoPreAprobado = 1000000 };
			CrearServicioFinancieroService _service = new CrearServicioFinancieroService(new UnitOfWork(_context));
			var response = _service.Ejecutar(request);
			Assert.AreEqual($"El número de cuenta ya exite", response.Mensaje);
		}

	}
}