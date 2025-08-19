using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BaseRepository
    {
        protected readonly SqlConnection _connection;

        protected BaseRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Asegura que la conexión a la base de datos esté abierta antes de ejecutar cualquier comando.
        /// Si la conexión no está abierta, la abre de forma asíncrona.
        /// </summary>
        /// <returns></returns>
        protected async Task EnsureConnectionOpenAsync()
        {
            if (_connection.State != ConnectionState.Open)
                await _connection.OpenAsync();
        }

    }
}
