
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationCoreMVC.ModelsDTO;

namespace WebApplicationCoreMVC.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            try 
            {
                List<ProductDTO> products = await _productService.GetAllProductsAsync();

                if (products == null || !products.Any())
                {
                    return NotFound("There are not products");
                }

                List<ProductWebAppCoreMVC_DTO> productsWebAppCoreMVC_DTO = products.Select(product => new ProductWebAppCoreMVC_DTO
                {
                    Codigo = product.Codigo,
                    NombreProducto = product.NombreProducto,
                    Descripcion = product.Descripcion,
                    Stock = product.Stock,
                    FechaVencimiento = product.FechaVencimiento
                }).ToList();

                return View(productsWebAppCoreMVC_DTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error interno del servidor", detail = ex.Message });
            }
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductWebAppCoreMVC_DTO prod)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (prod == null)
                {
                    return NotFound("collection is empty");
                }

                ProductDTO _productServiceDTO = new ProductDTO
                {
                    Id = prod.Id,
                    Codigo = prod.Codigo,
                    NombreProducto = prod.NombreProducto,
                    Descripcion = prod.Descripcion,
                    Stock = prod.Stock,
                    FechaVencimiento = prod.FechaVencimiento
                };


                int response = await _productService.CreateProductAsync(_productServiceDTO);//Id del producto creado

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
