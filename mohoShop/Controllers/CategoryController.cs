using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using mohoShop.Dto;
using mohoShop.Interfaces;
using mohoShop.Models;

namespace mohoShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategories(int page)
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories(page));

            return Ok(categories);
        }

        [HttpGet("Products/{categoryId}")]
        public IActionResult GetProductsByCategory(int categoryId)
        {
            var categories = _mapper.Map<List<ProductDto>>(_categoryRepository.GetProducts(categoryId));

            return Ok(categories);
        }

        [HttpGet("{categoryName}")]
        public IActionResult GetCategoryByName(string categoryName)
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategoryByName(categoryName));

            return Ok(categories);
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryDtoNoId categoryCreate)
        {
            // Check empty
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check duplicated
            if (_categoryRepository.CategoryNameExists(categoryCreate.Name))
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{categoryId}")]
        public IActionResult UpdateCategory(int categoryId, CategoryDtoNoId model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var category = _mapper.Map<Category>(model);

            category.Id = categoryId;
            category.Name = model.Name;

            if (!_categoryRepository.UpdateCategory(category))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        [HttpDelete("{categoryId}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            if (!_categoryRepository.DeleteCategory(categoryId))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}
