﻿using Domain.Base;
using Domain.Contracts;
using Domain.Entities;
using System;

namespace Application
{
    public class CrearCuentaBancariaService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly ServicioFinancieroFactory servicioFinancieroFactory;
        
        public CrearCuentaBancariaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            servicioFinancieroFactory = ServicioFinancieroFactory.CrearServicioFinancieroFactory(0);
        }
        public CrearCuentaBancariaResponse Ejecutar(CrearCuentaBancariaRequest request)
        {
            CuentaBancaria cuenta = _unitOfWork.CuentaBancariaRepository.FindFirstOrDefault(t => t.Numero==request.Numero);
            if (cuenta == null)
            {                
                CuentaBancaria cuentaNueva = (CuentaBancaria) servicioFinancieroFactory.GetServicioFinanciero(request.TipoCuenta);//Debe ir un factory que determine que tipo de cuenta se va a crear
                cuentaNueva.Nombre = request.Nombre;
                cuentaNueva.Numero = request.Numero;
                _unitOfWork.CuentaBancariaRepository.Add(cuentaNueva);
                _unitOfWork.Commit();
                return new CrearCuentaBancariaResponse() { Mensaje = $"Se creó con exito la cuenta {cuentaNueva.Numero} de tipo {cuentaNueva.GetType().Name}" };
            }
            else
            {
                return new CrearCuentaBancariaResponse() { Mensaje = $"El número de cuenta ya exite" };
            }
        }

        

    }
    public class CrearCuentaBancariaRequest
    {
        public string Nombre { get; set; }
        public int TipoCuenta { get; set; }
        public string Numero { get; set; }
    }
    public class CrearCuentaBancariaResponse
    {
        public string Mensaje { get; set; }
    }
}
