using Application;
using Domain.Contracts;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaBancariaController : ControllerBase
    {
        readonly BancoContext _context;
        readonly IUnitOfWork _unitOfWork;
        
        //Se Recomienda solo dejar la Unidad de Trabajo
        public CuentaBancariaController(BancoContext context,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        [HttpPost]
        public ActionResult<CrearCuentaBancariaResponse> Post(CrearCuentaBancariaRequest request)
        {
            CrearServicioFinancieroService _service = new CrearServicioFinancieroService(_unitOfWork);
            CrearCuentaBancariaResponse response = _service.Ejecutar(request);
            return Ok(response);
        }

        [HttpPost("consignacion")]
        public ActionResult<ConsignarResponse> Post(ConsignarRequest request)
        {
            var _service = new ConsignarService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            return Ok(response);
        }
        [HttpPost("retiro")]
        public ActionResult<RetirarResponse> Post(RetirarRequest request)
        {
            var _service = new RetirarService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            return Ok(response);
        }
    }
}