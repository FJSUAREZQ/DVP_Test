using Domain.Entities.Create;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFacturaService
    {
        Task<FacturaCrearResult> CreateAsync(FacturaCrear factura);
        Task<IEnumerable<Factura>> GetByNumeFacturaAsync(string numFactura);
        Task<IEnumerable<Factura>> GetByClienteIdAsync(int clienteId);
        Task<IEnumerable<FacturaDetalle>> GetDetailByNumFacturaAsync(string numFactura);
    }
}
