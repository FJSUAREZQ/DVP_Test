using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.DTOs.Create;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturaRepository _repo;
        public FacturasController(IFacturaRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Crear([FromBody] FacturaCrearDTO facturaCrear)
        {
            if (facturaCrear == null)
            {
                return BadRequest(new { message = "Datos de factura inválidos." });
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var factura = new Domain.Entities.Create.FacturaCrear
                {
                    NumeroFactura = facturaCrear.NumeroFactura,
                    ClienteId = facturaCrear.ClienteId,
                    UsuarioId = facturaCrear.UsuarioId,
                    Subtotal = facturaCrear.Subtotal,
                    Impuestos = facturaCrear.Impuestos,
                    Total = facturaCrear.Total,
                    Detalles = facturaCrear.Detalles.Select(d => new Domain.Entities.Create.FacturaDetalleLinea
                    {
                        ProductoId = d.ProductoId,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        TotalLinea = d.TotalLinea
                    }).ToList()
                };

                var result = await _repo.CreateAsync(factura);

                if (result.Exito)
                {
                    return Ok(new { numeroFactura = result.NumeroFactura });
                }
                else
                {
                    return BadRequest(new { message = result.Mensaje });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error inesperado", detalle = ex.Message });
            }
        }


        [HttpGet("buscarNumFactura/{numeroFactura}")]
        public async Task<ActionResult<IEnumerable<FacturaDTO>>> BuscarByNumFactura(string numeroFactura)
        {
            if (string.IsNullOrEmpty(numeroFactura))
            {
                return BadRequest(new { message = "Número de factura inválido." });
            }

            try
            {
                var facturas = await _repo.GetByNumeFacturaAsync(numeroFactura);

                if (facturas == null)
                {
                    return NotFound(new { message = "Factura no encontrada." });
                }

                var facturaDTOs = facturas.Select(f => new FacturaDTO
                {
                    FacturaId = f.FacturaId,
                    NumeroFactura = f.NumeroFactura,
                    ClienteId = f.ClienteId,
                    Cliente = f.Cliente,
                    Email = f.Email ?? string.Empty,
                    Direccion = f.Direccion ?? string.Empty,
                    Ciudad = f.Ciudad ?? string.Empty,
                    Departamento = f.Departamento ?? string.Empty,
                    Subtotal = f.Subtotal,
                    Impuestos = f.Impuestos,
                    Total = f.Total,
                    FechaCreacion = f.FechaCreacion

                }).ToList();


                return Ok(facturaDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error inesperado", detalle = ex.Message });
            }
        }


        [HttpGet("buscarClienteId/{clienteId:int}")]
        public async Task<ActionResult<IEnumerable<FacturaDTO>>> BuscarByClienteId(int clienteId)
        {
            if (clienteId <= 0)
            {
                return BadRequest(new { message = "ID de cliente inválido." });
            }
            try
            {
                var facturas = await _repo.GetByClienteIdAsync(clienteId);
                if (facturas == null )
                {
                    return NotFound(new { message = "No se encontraron facturas para el cliente especificado." });
                }
               
                var facturaDTOs = facturas.Select(f => new FacturaDTO
                {
                    FacturaId = f.FacturaId,
                    NumeroFactura = f.NumeroFactura,
                    ClienteId = f.ClienteId,
                    Cliente = f.Cliente,
                    Email = f.Email ?? string.Empty,
                    Direccion = f.Direccion ?? string.Empty,
                    Ciudad = f.Ciudad ?? string.Empty,
                    Departamento = f.Departamento ?? string.Empty,
                    Subtotal = f.Subtotal,
                    Impuestos = f.Impuestos,
                    Total = f.Total,
                    FechaCreacion = f.FechaCreacion

                }).ToList();

                return Ok(facturaDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error inesperado", detalle = ex.Message });
            }
        }


        [HttpGet("detalles/{numFactura}")]
        public async Task<ActionResult<IEnumerable<FacturaDetalleDTO>>> GetDetallesByNumFactura(string numFactura)
        {
            if (string.IsNullOrEmpty(numFactura))
            {
                return BadRequest(new { message = "Número de factura inválido." });
            }
            try
            {
                var detalles = await _repo.GetDetailByNumFacturaAsync(numFactura);
                if (detalles == null || !detalles.Any())
                {
                    return NotFound(new { message = "No se encontraron detalles para la factura especificada." });
                }
                
                var detalleDTOs = detalles.Select(d => new FacturaDetalleDTO
                {
                    DetalleId = d.DetalleId,
                    FacturaId = d.FacturaId,
                    ProductoId = d.ProductoId,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    TotalLinea = d.TotalLinea,
                    FechaCreacion = d.FechaCreacion

                }).ToList();
                return Ok(detalleDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error inesperado", detalle = ex.Message });
            }
        }

    }
}
