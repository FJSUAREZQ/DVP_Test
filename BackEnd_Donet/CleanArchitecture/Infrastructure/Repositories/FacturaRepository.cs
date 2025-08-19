using Domain.Entities;
using Domain.Entities.Create;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FacturaRepository : BaseRepository, IFacturaRepository
    {
        /// <summary>
        /// Constructor que recibe la conexión SQL y la pasa a la clase base.
        /// </summary>
        /// <param name="connection"></param>
        public FacturaRepository(SqlConnection connection) : base(connection) { }

        /// <summary>
        /// Crea una nueva factura utilizando el procedimiento almacenado SP_Factura_Crear.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<FacturaCrearResult> CreateAsync(FacturaCrear request)
        {
            var result = new FacturaCrearResult();

            try
            {
                await EnsureConnectionOpenAsync();

                using var cmd = _connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_Factura_Crear";

                cmd.Parameters.AddWithValue("@NumeroFactura", request.NumeroFactura);
                cmd.Parameters.AddWithValue("@ClienteId", request.ClienteId);
                cmd.Parameters.AddWithValue("@UsuarioId", request.UsuarioId);
                cmd.Parameters.AddWithValue("@Subtotal", request.Subtotal);
                cmd.Parameters.AddWithValue("@Impuestos", request.Impuestos);
                cmd.Parameters.AddWithValue("@Total", request.Total);

                // TVP
                var detallesTable = new DataTable();
                detallesTable.Columns.Add("ProductoId", typeof(int));
                detallesTable.Columns.Add("Cantidad", typeof(int));
                detallesTable.Columns.Add("PrecioUnitario", typeof(decimal));
                detallesTable.Columns.Add("TotalLinea", typeof(decimal));

                foreach (var d in request.Detalles)
                {
                    detallesTable.Rows.Add(d.ProductoId, d.Cantidad, d.PrecioUnitario, d.TotalLinea);
                }

                var detallesParam = cmd.Parameters.AddWithValue("@Detalles", detallesTable);
                detallesParam.SqlDbType = SqlDbType.Structured;
                detallesParam.TypeName = "FacturaDetalleType";

                using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    result.Exito = true;
                    result.NumeroFactura = reader.GetString(reader.GetOrdinal("NumeroFactura"));
                }
                else
                {
                    result.Exito = false;
                    result.Mensaje = "No se recibió respuesta del procedimiento.";
                }
            }
            catch (SqlException ex)
            {
                result.Exito = false;
                result.Mensaje = $"Error SQL: {ex.Message}";
            }
            catch (Exception ex)
            {
                result.Exito = false;
                result.Mensaje = $"Error general: {ex.Message}";
            }

            return result;
        }


        /// <summary>
        /// Busca una factura por su número utilizando el procedimiento almacenado SP_Factura_BuscarByNumero.
        /// </summary>
        /// <param name="numFactura"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Factura>> GetByNumeFacturaAsync(string numFactura)
        {
            var facturas = new List<Factura>();
            await EnsureConnectionOpenAsync();
            using var cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Factura_BuscarByNumero";
            cmd.Parameters.AddWithValue("@NumeroFactura", numFactura);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                facturas.Add(new Factura
                {
                    FacturaId = reader.GetInt32(reader.GetOrdinal("FacturaId")),
                    NumeroFactura = reader.GetString(reader.GetOrdinal("NumeroFactura")),
                    ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Cliente = reader.GetString(reader.GetOrdinal("Cliente")),
                    Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                    Ciudad = reader.GetString(reader.GetOrdinal("Ciudad")),
                    Departamento = reader.GetString(reader.GetOrdinal("Departamento")),
                    Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal")),
                    Impuestos = reader.GetDecimal(reader.GetOrdinal("Impuestos")),
                    Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo")),
                    UsuarioCreacion = reader.GetString(reader.GetOrdinal("UsuarioCreacion")),
                    FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                    UsuarioModificacion = reader.IsDBNull(reader.GetOrdinal("UsuarioModificacion")) ? null : reader.GetString(reader.GetOrdinal("UsuarioModificacion")),
                    FechaModificacion = reader.IsDBNull(reader.GetOrdinal("FechaModificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaModificacion"))

                });
            }

            return facturas;
        }

        /// <summary>
        /// Obtiene todas las facturas asociadas a un cliente específico utilizando el procedimiento almacenado SP_Factura_BuscarByClienteId.
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Factura>> GetByClienteIdAsync(int clienteId)
        {
            var facturas = new List<Factura>();
            await EnsureConnectionOpenAsync();
            using var cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Factura_BuscarByClienteId";
            cmd.Parameters.AddWithValue("@ClienteId", clienteId);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                facturas.Add(new Factura
                {
                    FacturaId = reader.GetInt32(reader.GetOrdinal("FacturaId")),
                    NumeroFactura = reader.GetString(reader.GetOrdinal("NumeroFactura")),
                    ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Cliente = reader.GetString(reader.GetOrdinal("Cliente")),
                    Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                    Ciudad = reader.GetString(reader.GetOrdinal("Ciudad")),
                    Departamento = reader.GetString(reader.GetOrdinal("Departamento")),
                    Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal")),
                    Impuestos = reader.GetDecimal(reader.GetOrdinal("Impuestos")),
                    Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo")),
                    UsuarioCreacion = reader.GetString(reader.GetOrdinal("UsuarioCreacion")),
                    FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                    UsuarioModificacion = reader.IsDBNull(reader.GetOrdinal("UsuarioModificacion")) ? null : reader.GetString(reader.GetOrdinal("UsuarioModificacion")),
                    FechaModificacion = reader.IsDBNull(reader.GetOrdinal("FechaModificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaModificacion"))

                });
            }
            return facturas;
        }

        /// <summary>
        /// Obtiene los detalles de una factura específica utilizando el procedimiento almacenado SP_Factura_ListarDetalleByNumFactura.
        /// </summary>
        /// <param name="numFactura"></param>
        /// <returns></returns>
        public async Task<IEnumerable<FacturaDetalle>> GetDetailByNumFacturaAsync(string numFactura)
        {
            var detalles = new List<FacturaDetalle>();
            await EnsureConnectionOpenAsync();
            using var cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Factura_ListarDetalleByNumFactura";
            cmd.Parameters.AddWithValue("@NumeroFactura", numFactura);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                detalles.Add(new FacturaDetalle
                {
                    DetalleId = reader.GetInt32(reader.GetOrdinal("DetalleId")),
                    FacturaId = reader.GetInt32(reader.GetOrdinal("FacturaId")),
                    ProductoId = reader.GetInt32(reader.GetOrdinal("ProductoId")),
                    Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                    PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario")),
                    TotalLinea = reader.GetDecimal(reader.GetOrdinal("TotalLinea")),
                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo")),
                    UsuarioCreacion = reader.GetString(reader.GetOrdinal("UsuarioCreacion")),
                    FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                    UsuarioModificacion = reader.IsDBNull(reader.GetOrdinal("UsuarioModificacion")) ? null : reader.GetString(reader.GetOrdinal("UsuarioModificacion")),
                    FechaModificacion = reader.IsDBNull(reader.GetOrdinal("FechaModificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaModificacion"))

                });
            }
            return detalles;
        }




    }
}
