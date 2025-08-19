
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductoService : IProductoService
    {

        private readonly IProductoRepository _productRepository;
        

        public ProductoService(IProductoRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Obtiene todos los productos de la base de datos.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        /// <summary>
        /// Obtiene todos los productos para un dropdown list.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductosCombos>> GetAllForDropListAsync()
        {
            return await _productRepository.GetAllForDropListAsync();
        }

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Producto> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }


    }
}
