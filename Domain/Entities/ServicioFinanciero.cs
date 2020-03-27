using Domain.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public abstract class ServicioFinanciero : Entity<int>, IServicioFinanciero
    {
        protected ServicioFinanciero()
        {
            Movimientos = new List<MovimientoFinanciero>();
            this.FechaCreacion = DateTime.Now;
        }

        public List<MovimientoFinanciero> Movimientos { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public decimal Saldo { get;  set; }
        public string Ciudad { get; set; }
        public DateTime FechaCreacion { get; }

        double IServicioFinanciero.Saldo => throw new NotImplementedException();

        public abstract string Consignar(decimal valor, string ciudadDeOrigen);
                       
        protected void EjecutarConsignacion(decimal valor)
        {
            MovimientoFinanciero movimiento = new MovimientoFinanciero();
            movimiento.ValorConsignacion = valor;
            movimiento.FechaMovimiento = DateTime.Now;
            Saldo += valor;
            Movimientos.Add(movimiento);
        }
        public abstract string Retirar(decimal valor);        

        protected void EjecutarRetiro(decimal valor)
        {
            MovimientoFinanciero retiro = new MovimientoFinanciero();
            retiro.ValorRetiro = valor;
            retiro.FechaMovimiento = DateTime.Now;
            Saldo -= valor;
            this.Movimientos.Add(retiro);
        }

        public override string ToString()
        {
            return ($"Su saldo disponible es {Saldo}.");
        }

        protected List<MovimientoFinanciero> ObtenerRetiros()
        {
            return this.Movimientos.FindAll(movimiento => movimiento.ValorRetiro != 0);
        }

        protected List<MovimientoFinanciero> ObtenerRetirosPorFecha(int mes, int anio)
        {
            return this.ObtenerRetiros().FindAll(movimiento => movimiento.FechaMovimiento.Month == mes &&
                                                                movimiento.FechaMovimiento.Year == anio);
        }

        public string Trasladar(IServicioFinanciero servicioFinanciero, decimal valor)
        {
            Retirar(valor);
            servicioFinanciero.Consignar(valor, "");
            return "";
        }
        
    }
}
