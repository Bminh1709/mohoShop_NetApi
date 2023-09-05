using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mohoShop.Dto;
using mohoShop.Interfaces;
using mohoShop.Models;
using mohoShop.Repositories;

namespace mohoShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IBufferedFileUploadService _bufferedFileUploadRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper, IBufferedFileUploadService bufferedFileUploadService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _bufferedFileUploadRepository = bufferedFileUploadService;
        }

        [HttpGet]
        public IActionResult GetProducts(int page)
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProducts(page));

            return Ok(products);
        }

        [HttpGet("{ProductName}")]
        public IActionResult GetProductsByName(string ProductName)
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProductsByName(ProductName));

            return Ok(products);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromForm] ProductDtoWCatId product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (product.FormFile == null || product.FormFile.Length == 0)
            {
                ModelState.AddModelError("Thumbnail", "Thumbnail is required.");
                return BadRequest(ModelState);
            }

            string thumbnailPath = _bufferedFileUploadRepository.UploadFile(product.FormFile);

            if (string.IsNullOrEmpty(thumbnailPath))
            {
                // Handle the case where file upload failed.
                ModelState.AddModelError("Thumbnail", "Thumbnail upload failed.");
                return BadRequest(ModelState);
            }

            product.Thumbnail = thumbnailPath;
            product.CreateAt = DateTime.Now;

            var productMap = _mapper.Map<Product>(product);

            if (!_categoryRepository.CategoryExists(product.CategoryId))
                return NotFound("Invalid category");

            productMap.Category = _categoryRepository.GetCategoryById(product.CategoryId);

            if (!_productRepository.CreateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut]
        public IActionResult UpdateProduct([FromForm] ProductDtoUpdate updateProduct)
        {
            if (!_productRepository.ProductExists(updateProduct.Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (updateProduct.FormFile != null)
            {
                string thumbnailPath = _bufferedFileUploadRepository.UploadFile(updateProduct.FormFile);

                if (string.IsNullOrEmpty(thumbnailPath))
                {
                    // Handle the case where file upload failed.
                    ModelState.AddModelError("Thumbnail", "Thumbnail upload failed.");
                    return BadRequest(ModelState);
                }

                updateProduct.Thumbnail = thumbnailPath;
            }

            updateProduct.UpdateAt = DateTime.Now;

            // var existingProduct = _productRepository.GetProductById(updateProduct.Id);
            var existingProduct = _mapper.Map<Product>(updateProduct);

            if (updateProduct.CategoryId != null)
            {
                if (!_categoryRepository.CategoryExists((int)updateProduct.CategoryId))
                    return NotFound("Invalid category");

                existingProduct.Category = _categoryRepository.GetCategoryById((int)updateProduct.CategoryId);
            }

            if (!_productRepository.UpdateProduct(existingProduct))
            {   
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int productId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.ProductExists(productId))
                return NotFound();

            if (!_productRepository.DeleteProduct(productId))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}
