using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private readonly IClienteRepository _repo;

        public ClientesController(IClienteRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<ClienteDTO>> Listar()
        {
            try
            {
                var clientes = await _repo.GetAllAsync();

                if (clientes == null || !clientes.Any())
                {
                    return NotFound(new { message = "No se encontraron clientes." });
                }

                // Si se encontraron clientes, retornamos el dto
                List<ClienteDTO> clientesDTO = clientes.Select(c => new ClienteDTO
                {
                    ClienteId = c.ClienteId,
                    Nombre = c.Nombre,
                    Email = c.Email,
                    Telefono = c.Telefono,
                    Direccion = c.Direccion,
                    Ciudad = c.Ciudad,
                    Departamento = c.Departamento
                }).ToList();

                return Ok(clientesDTO);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { error = "Error al obtener clientes", detalle = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error inesperado", detalle = ex.Message });
            }
        }

        [HttpGet("listarCombos")]
        public async Task<ActionResult<ClientesCombosDTO>> ListarByCombos()
        {
            try
            {
                var clientesCombos = await _repo.GetAllForDropListAsync();

                if (clientesCombos == null || !clientesCombos.Any())
                {
                    return NotFound(new { message = "No se encontraron clientes." });
                }

                List<ClientesCombosDTO> clientesCombosDTO = clientesCombos.Select(c => new ClientesCombosDTO
                {
                    ClienteId = c.ClienteId,
                    Nombre = c.Nombre
                }).ToList();

                return Ok(clientesCombosDTO);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { error = "Error al obtener clientes para los combos", detalle = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error inesperado", detalle = ex.Message });
            }
        }


    }
}
