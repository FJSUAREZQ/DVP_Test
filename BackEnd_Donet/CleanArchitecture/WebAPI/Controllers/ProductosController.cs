using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepository _repo;
        public ProductosController(IProductoRepository repo)
        {
            _repo = repo;
        }


        [HttpGet("listar")]
        public async Task<ActionResult<ProductoDTO>> Listar()
        {
            try
            {
                var productos = await _repo.GetAllAsync();
                if (productos == null || !productos.Any())
                {
                    return NotFound(new { message = "No se encontraron productos." });
                }
                // Si se encontraron productos, retornamos el dto
                List<ProductoDTO> productosDTO = productos.Select(p => new ProductoDTO
                {
                    ProductoId = p.ProductoId,
                    Nombre = p.Nombre,
                    PrecioUnitario = p.PrecioUnitario,
                    ImagenUrl = p.ImagenUrl,
                    Stock = p.Stock
                }).ToList();
                return Ok(productosDTO);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { error = "Error al obtener productos", detalle = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error inesperado", detalle = ex.Message });
            }
        }


        [HttpGet("listarCombos")]
        public async Task<ActionResult<ProductosCombosDTO>> ListarByCombos()
        {
            try
            {
                var productos = await _repo.GetAllAsync();

                if (productos == null || !productos.Any())
                {
                    return NotFound(new { message = "No se encontraron productos." });
                }
                // Si se encontraron productos, retornamos el dto
                List<ProductosCombosDTO> productosComboDTO = productos.Select(p => new ProductosCombosDTO
                {
                    ProductoId = p.ProductoId,
                    Nombre = p.Nombre
                }).ToList();
                return Ok(productosComboDTO);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { error = "Error al obtener productos", detalle = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error inesperado", detalle = ex.Message });
            }
        }


        [HttpGet("buscar/{id}")]
        public async Task<ActionResult<ProductoDTO>> Buscar(int id)
        {
            try
            {
                var producto = await _repo.GetByIdAsync(id);

                if (producto == null)
                {
                    return NotFound(new { message = "Producto no encontrado." });
                }

                ProductoDTO productoDTO = new ProductoDTO
                {
                    ProductoId = producto.ProductoId,
                    Nombre = producto.Nombre,
                    PrecioUnitario = producto.PrecioUnitario,
                    ImagenUrl = producto.ImagenUrl,
                    Stock = producto.Stock
                };
                return Ok(productoDTO);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { error = "Error al buscar el producto", detalle = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error inesperado", detalle = ex.Message });
            }
        }
    }
}
