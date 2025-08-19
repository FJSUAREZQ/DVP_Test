using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ClienteRepository : BaseRepository, IClienteRepository
    {
        /// <summary>
        /// Constructor que recibe la conexión SQL y la pasa a la clase base.
        /// </summary>
        /// <param name="connection"></param>
        public ClienteRepository(SqlConnection connection) : base(connection) { }


        /// <summary>
        /// Obtiene todos los clientes de la base de datos.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            var clientes = new List<Cliente>();
            await EnsureConnectionOpenAsync();

            using var cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Clientes_Listar";

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                clientes.Add(new Cliente
                {
                    ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                    Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString(reader.GetOrdinal("Direccion")),
                    Ciudad = reader.IsDBNull(reader.GetOrdinal("Ciudad")) ? null : reader.GetString(reader.GetOrdinal("Ciudad")),
                    Departamento = reader.IsDBNull(reader.GetOrdinal("Departamento")) ? null : reader.GetString(reader.GetOrdinal("Departamento")),
                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo")),
                    UsuarioCreacion = reader.GetString(reader.GetOrdinal("UsuarioCreacion")),
                    FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                    UsuarioModificacion = reader.IsDBNull(reader.GetOrdinal("UsuarioModificacion")) ? null : reader.GetString(reader.GetOrdinal("UsuarioModificacion")),
                    FechaModificacion = reader.IsDBNull(reader.GetOrdinal("FechaModificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaModificacion"))

                });
            }

            return clientes;
        }


        /// <summary>
        /// Obtiene una lista de clientes básicos para un dropdown.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ClienteCombos>> GetAllForDropListAsync()
        {
            var clientes = new List<ClienteCombos>();
            await EnsureConnectionOpenAsync();

            using var cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Clientes_ListarForCombos";

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                clientes.Add(new ClienteCombos
                {
                    ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre"))
                });
            }

            return clientes;
        }
    }



}
