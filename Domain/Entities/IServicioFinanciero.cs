
namespace Domain.Entities
{
    public interface 
        IServicioFinanciero
    {

        string Nombre { get; set; }
        string Numero { get; set; }
        double Saldo { get; }

        string Retirar(decimal valor);
        string Consignar(decimal valor, string ciudadDeOrigen);
        string Trasladar(IServicioFinanciero servicioFinanciero, decimal valor);

    }
}
