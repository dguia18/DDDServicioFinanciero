
namespace Domain.Entities
{
	public interface
		IServicioFinanciero
	{

		string Nombre { get; set; }
		string Numero { get; set; }
		double Saldo { get; }

		string Retirar(double valor);
		string Consignar(double valor, string ciudadDeOrigen);
		string Trasladar(IServicioFinanciero servicioFinanciero, double valor);

	}
}
