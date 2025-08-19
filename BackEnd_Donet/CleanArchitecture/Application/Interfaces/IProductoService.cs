
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<IEnumerable<ProductosCombos>> GetAllForDropListAsync();
        Task<Producto> GetByIdAsync(int id);

    }
}
