
namespace Domain.Entities
{
    public interface IServicioFinanciero
    {

        string Nombre { get; set; }
        string Numero { get; set; }
        double Saldo { get; }

        void Retirar(decimal valor);
        void Consignar(decimal valor);
        void Trasladar(IServicioFinanciero servicioFinanciero, decimal valor);

    }
}
