using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Create;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _facturaRepository;
        public FacturaService(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        /// <summary>
        /// Crea una nueva factura en la base de datos.
        /// </summary>
        /// <param name="factura"></param>
        /// <returns></returns>
        public async Task<FacturaCrearResult> CreateAsync(FacturaCrear factura)
        {
            return await _facturaRepository.CreateAsync(factura);
        }

        /// <summary>
        /// Obtiene una factura por su número de factura.
        /// </summary>
        /// <param name="numFactura"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Factura>> GetByNumeFacturaAsync(string numFactura)
        {
            return await _facturaRepository.GetByNumeFacturaAsync(numFactura);
        }

        /// <summary>
        /// Obtiene todas las facturas asociadas a un cliente por su ID.
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Factura>> GetByClienteIdAsync(int clienteId)
        {
            return await _facturaRepository.GetByClienteIdAsync(clienteId);
        }

        /// <summary>
        /// Obtiene los detalles de una factura por su número de factura.
        /// </summary>
        /// <param name="numFactura"></param>
        /// <returns></returns>
        public async Task<IEnumerable<FacturaDetalle>> GetDetailByNumFacturaAsync(string numFactura)
        {
            return await _facturaRepository.GetDetailByNumFacturaAsync(numFactura);
        }



    }
}
