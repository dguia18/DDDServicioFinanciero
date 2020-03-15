using Domain.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public abstract class CuentaBancaria : Entity<int>, IServicioFinanciero
    {
        protected CuentaBancaria()
        {
            Movimientos = new List<MovimientoFinanciero>();
        }

        public List<MovimientoFinanciero> Movimientos { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public decimal Saldo { get; protected set; }

        double IServicioFinanciero.Saldo => throw new NotImplementedException();

        public virtual void Consignar(decimal valor)
        {
            MovimientoFinanciero movimiento = new MovimientoFinanciero();
            movimiento.ValorConsignacion = valor;
            movimiento.FechaMovimiento = DateTime.Now;
            Saldo += valor;
            Movimientos.Add(movimiento);
        }                

        public void Retirar(decimal valor)
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

        public void Trasladar(IServicioFinanciero servicioFinanciero, decimal valor)
        {
            Retirar(valor);
            servicioFinanciero.Consignar(valor);
        }
        
    }
}
