using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductoRepository : BaseRepository,IProductoRepository
    {
        /// <summary>
        /// Constructor que recibe la conexión SQL y la pasa a la clase base.
        /// </summary>
        /// <param name="connection"></param>
        public ProductoRepository(SqlConnection connection) : base(connection) { }


        /// <summary>
        /// Obtiene todos los productos de la base de datos.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            var productos = new List<Producto>();
            await EnsureConnectionOpenAsync();

            using var cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Productos_Listar";

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                productos.Add(new Producto
                {
                    ProductoId = reader.GetInt32(reader.GetOrdinal("ProductoId")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario")),
                    ImagenUrl = reader.IsDBNull(reader.GetOrdinal("ImagenUrl")) ? null : reader.GetString(reader.GetOrdinal("ImagenUrl")),
                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo")),
                    Stock = reader.GetDecimal(reader.GetOrdinal("Stock")),
                    UsuarioCreacion = reader.GetString(reader.GetOrdinal("UsuarioCreacion")),
                    FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                    UsuarioModificacion = reader.IsDBNull(reader.GetOrdinal("UsuarioModificacion")) ? null : reader.GetString(reader.GetOrdinal("UsuarioModificacion")),
                    FechaModificacion = reader.IsDBNull(reader.GetOrdinal("FechaModificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaModificacion"))
                });
            }

            return productos;
        }

        /// <summary>
        /// Obtiene todos los productos para poblar dropdown list.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductosCombos>> GetAllForDropListAsync()
        {
            var productos = new List<ProductosCombos>();
            await EnsureConnectionOpenAsync();

            using var cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Productos_ListarForCombos";

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                productos.Add(new ProductosCombos
                {
                    ProductoId = reader.GetInt32(reader.GetOrdinal("ProductoId")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre"))
                });
            }

            return productos;
        }


        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="productoId"></param>
        /// <returns></returns>
        public async Task<Producto?> GetByIdAsync(int productoId)
        {
            await EnsureConnectionOpenAsync();

            using var cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Productos_ProductoByID";

            cmd.Parameters.AddWithValue("@ProductoId", productoId);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Producto
                {
                    ProductoId = reader.GetInt32(reader.GetOrdinal("ProductoId")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario")),
                    ImagenUrl = reader.IsDBNull(reader.GetOrdinal("ImagenUrl")) ? null : reader.GetString(reader.GetOrdinal("ImagenUrl")),
                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo")),
                    Stock = reader.GetDecimal(reader.GetOrdinal("Stock")),
                    UsuarioCreacion = reader.GetString(reader.GetOrdinal("UsuarioCreacion")),
                    FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                    UsuarioModificacion = reader.IsDBNull(reader.GetOrdinal("UsuarioModificacion")) ? null : reader.GetString(reader.GetOrdinal("UsuarioModificacion")),
                    FechaModificacion = reader.IsDBNull(reader.GetOrdinal("FechaModificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaModificacion"))
                };
            }

            return null;
        }


    }
}
