using Domain.Entities;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
	public class BancoContext : DbContextBase
	{
		public BancoContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<CuentaAhorro> CuentasAhorro { get; set; }
		public DbSet<CuentaCorriente> CuentasCorriente { get; set; }
		public DbSet<CertificadoDeDepositoATermino> CertificadosDeDepositosATermino { get; set; }
		public DbSet<TarjetaDeCredito> TarjetasDeCredito { get; set; }
		public DbSet<MovimientoFinanciero> MovimientosFinancieros { get; set; }

	}
}
