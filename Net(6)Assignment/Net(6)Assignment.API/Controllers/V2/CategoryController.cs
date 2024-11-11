#pragma warning restore CS1591
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Net_6_Assignment.Common.Enums;
using Net_6_Assignment.Common.Exceptions;
using Net_6_Assignment.DTOs;
using Net_6_Assignment.Models;
using Net_6_Assignment.Services;

namespace Net_6_Assignment.Controllers.V2
{
    [ApiExplorerSettings(GroupName = nameof(APIVersion.V2))]
    [ApiController]
    [Route("api/v2/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IService<Category> _CategoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IService<Category> CategoryService, IMapper mapper, ILogger<CategoryController> logger)
        {
            _CategoryService = CategoryService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all category items.
        /// </summary>
        /// <returns>A list of all category items.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponseDTO>>> GetAllCategorys()
        {
            _logger.LogInformation("Retrieving all categories.");
            IEnumerable<Category> CategoryList = await _CategoryService.GetAllAsync();
            IEnumerable<CategoryResponseDTO> res = _mapper.Map<IEnumerable<CategoryResponseDTO>>(CategoryList);
            _logger.LogInformation("Successfully retrieved all categories. Total count: {Count}", CategoryList.Count());
            return Ok(res);
        }

        /// <summary>
        /// Retrieves a category item by its specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the category item.</param>
        /// <returns>The category item with the specified ID.</returns>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CategoryResponseDTO>> GetCategoryById(Guid id)
        {
            _logger.LogInformation("Retrieving category with ID: {Id}", id);
            Category? Category = await _CategoryService.GetByConditionAsync(category => category.Id == id);
            if (Category == null)
            {
                _logger.LogWarning("Category with ID: {Id} does not exist.", id);
                throw new A6NotFoundException("Category does not exist!");
            }
            CategoryResponseDTO res = _mapper.Map<CategoryResponseDTO>(Category);
            _logger.LogInformation("Successfully retrieved category with ID: {Id}", id);
            return Ok(res);
        }

        /// <summary>
        /// Creates a new category item.
        /// </summary>
        /// <param name="categoryInfo">The request DTO containing category item information.</param>
        /// <returns>The newly created category item, along with the URI to access it.</returns>
        [HttpPost]
        public async Task<ActionResult<CategoryResponseDTO>> CreateCategory([FromBody] CategoryRequestDTO categoryInfo)
        {
            _logger.LogInformation("Creating a new category.");
            Category category = _mapper.Map<Category>(categoryInfo);
            Category newCategory = await _CategoryService.CreateAsync(category);
            CategoryResponseDTO createdCategory = _mapper.Map<CategoryResponseDTO>(newCategory);
            _logger.LogInformation("Successfully created category with ID: {Id}", createdCategory.Id);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
        }

        /// <summary>
        /// Updates an existing category item.
        /// </summary>
        /// <param name="updatedCategory">The DTO containing the updated category item information.</param>
        /// <returns>The updated category item information.</returns>
        [HttpPut]
        public async Task<ActionResult<CategoryResponseDTO>> UpdateCategory([FromBody] CategoryUpdateRequestDTO updatedCategory)
        {
            Guid categoryId = updatedCategory.Id;
            _logger.LogInformation("Updating category with ID: {Id}", categoryId);

            Category? existingCategory = await _CategoryService.GetByConditionAsync(category => category.Id == categoryId);
            if (existingCategory == null)
            {
                _logger.LogWarning("Category with ID: {Id} does not exist. Cannot update.", categoryId);
                throw new A6NotFoundException("Category does not exist!");
            }

            Category mappedUpdatedCategory = _mapper.Map<Category>(updatedCategory);
            Category updatedDbCategory = await _CategoryService.UpdateAsync(existingCategory, mappedUpdatedCategory);
            CategoryResponseDTO updatedCategoryRes = _mapper.Map<CategoryResponseDTO>(updatedDbCategory);
            _logger.LogInformation("Successfully updated category with ID: {Id}", categoryId);
            return Ok(updatedCategoryRes);
        }

        /// <summary>
        /// Deletes a category item by its specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the category item.</param>
        /// <returns>A no-content response.</returns>
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            _logger.LogInformation("Attempting to delete category with ID: {Id}", id);

            Category? existingCategory = await _CategoryService.GetByConditionAsync(category => category.Id == id);
            if (existingCategory == null)
            {
                _logger.LogWarning("Category with ID: {Id} does not exist. Cannot delete.", id);
                throw new A6NotFoundException("Category does not exist!");
            }

            await _CategoryService.DeleteAsync(existingCategory);
            _logger.LogInformation("Successfully deleted category with ID: {Id}", id);
            return NoContent();
        }
    }
}