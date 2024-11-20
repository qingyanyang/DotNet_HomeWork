using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Net_6_Assignment.Common.Enums;
using Net_6_Assignment.Common.Exceptions;
using Net_6_Assignment.Controllers;
using Net_6_Assignment.DTOs;
using Net_6_Assignment.Models;
using Net_6_Assignment.Profiles;
using Net_6_Assignment.Services;
using System.Linq.Expressions;


namespace Net_6_Assignment.Test.Contoller.Test
{
    public class CategoryControllerTest
    {
        private readonly CategoryController _categoryController;
        private readonly Mock<IService<Category>> _mockCategoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;
        public CategoryControllerTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = config.CreateMapper();
            _logger = NullLogger<CategoryController>.Instance;

            _mockCategoryService = new Mock<IService<Category>>();
            _categoryController = new CategoryController(_mockCategoryService.Object, _mapper, _logger);
        }

        [Fact]
        public async Task GetAllTest_Should_Return_AllCategories()
        {
            //Arrange
            var categories = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), CategoryName = "Category 1",CategoryLevel = 0, ParentId = Guid.NewGuid() },
                new Category { Id = Guid.NewGuid(), CategoryName = "Category 2",CategoryLevel = 0, ParentId = Guid.NewGuid() }
            };
            _mockCategoryService.Setup(service => service.GetAllAsync()).ReturnsAsync(categories);

            //Act
            var result = await _categoryController.GetAllCategorys();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;

            Assert.IsType<List<CategoryResponseDTO>>(list?.Value);

            var listCategories = list.Value as List<CategoryResponseDTO>;

            Assert.Equal(2, listCategories?.Count);
        }

        [Theory]
        [InlineData("3d2b8791-eaee-450e-8614-819f2beb5ed9", "e2c2d9b7-18b8-4b21-94b1-3c19efc169ec")]
        [InlineData("8c7eaf27-e504-4925-8ace-2de88a7d5221", "cfc2c5a7-78b8-4b21-94b1-3c19efc179ab")]
        public async Task GetCategoryById_Should_Return_Category_ById(string validGuidStr, string invalidGuidStr)
        {
            // Arrange
            Guid validGuid = new Guid(validGuidStr);
            Guid invalidGuid = new Guid(invalidGuidStr);

            var categories = new List<Category>
            {
                new Category { Id = new Guid("3d2b8791-eaee-450e-8614-819f2beb5ed9"), CategoryName = "Category 1", CategoryLevel = 0, ParentId = null },
                new Category { Id = new Guid("8c7eaf27-e504-4925-8ace-2de88a7d5221"), CategoryName = "Category 2", CategoryLevel = 0, ParentId = null }
            };

            // Setup mock to return the category that matches the valid Guid
            _mockCategoryService.Setup(service => service.GetByConditionAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync((Expression<Func<Category, bool>> predicate) => categories.FirstOrDefault(predicate.Compile()));

            // Act
            var resultValid = await _categoryController.GetCategoryById(validGuid);
            Exception exception = await Assert.ThrowsAsync<A6NotFoundException>(
                () => _categoryController.GetCategoryById(invalidGuid)
            );

            // Assert
            Assert.IsType<OkObjectResult>(resultValid.Result);
            var okResultValid = resultValid.Result as OkObjectResult;
            Assert.NotNull(okResultValid?.Value);
            Assert.IsType<CategoryResponseDTO>(okResultValid.Value);

            Assert.Equal("Category does not exist!", exception.Message);// Check for the exception and its message
        }

        [Theory]
        [InlineData("3d2b8791-eaee-450e-8614-819f2beb5ed9", "e2c2d9b7-18b8-4b21-94b1-3c19efc169ec")]
        public async Task CreateCategory_Should_HandleCategoryCreation(string validGuidStr, string invalidGuidStr)
        {
            // Arrange
            Guid validGuid = new Guid(validGuidStr);
            Guid invalidGuid = new Guid(invalidGuidStr);

            // Setup mock data
            var existingParentCategory = new Category
            {
                Id = validGuid,
                CategoryName = "Existing Parent Category",
                CategoryLevel = CategoryLevel.ParentLevel
            };

            var newParentCategoryDTO = new CategoryRequestDTO
            {
                CategoryName = "New Parent Category",
                CategoryLevel = CategoryLevel.ParentLevel
            };

            var newDuplicateParentCategoryDTO = new CategoryRequestDTO
            {
                CategoryName = "Existing Parent Category", // Duplicate name
                CategoryLevel = CategoryLevel.ParentLevel
            };

            var newValidChildCategoryDTO = new CategoryRequestDTO
            {
                CategoryName = "New Valid Child Category",
                CategoryLevel = CategoryLevel.ChildLevel,
                ParentId = validGuid // Valid parent ID
            };

            var newInvalidChildCategoryDTO = new CategoryRequestDTO
            {
                CategoryName = "New Invalid Child Category",
                CategoryLevel = CategoryLevel.ChildLevel,
                ParentId = invalidGuid // Invalid parent ID
            };

            // Setup the mock service for retrieving categories
            _mockCategoryService.Setup(service => service.GetByConditionAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync((Expression<Func<Category, bool>> predicate) =>
                    predicate.Compile().Invoke(existingParentCategory) ? existingParentCategory : null);
            _mockCategoryService.Setup(service => service.CreateAsync(It.IsAny<Category>()))
                .ReturnsAsync((Category category) =>
                {
                    category.Id = Guid.NewGuid(); // Simulate creating a new category and assigning a new ID
                    return category;
                });
            // Act
            var resultParentValid = await _categoryController.CreateCategory(newParentCategoryDTO);
            var exceptionDuplicate = await Assert.ThrowsAsync<A6ArgumentException>(() => _categoryController.CreateCategory(newDuplicateParentCategoryDTO));
            var resultChildValid = await _categoryController.CreateCategory(newValidChildCategoryDTO);
            var exceptionInvalidParent = await Assert.ThrowsAsync<A6ArgumentException>(() => _categoryController.CreateCategory(newInvalidChildCategoryDTO));

            // Assert
            Assert.IsType<CreatedAtActionResult>(resultParentValid.Result);
            var createdResultParent = resultParentValid.Result as CreatedAtActionResult;
            Assert.NotNull(createdResultParent?.Value);
            Assert.IsType<CategoryResponseDTO>(createdResultParent.Value);

            Assert.Equal("The specified category already exist.", exceptionDuplicate.Message);

            Assert.IsType<CreatedAtActionResult>(resultChildValid.Result);
            var createdResultChild = resultChildValid.Result as CreatedAtActionResult;
            Assert.NotNull(createdResultChild?.Value);
            Assert.IsType<CategoryResponseDTO>(createdResultChild.Value);

            Assert.Equal("The specified parent category does not exist.", exceptionInvalidParent.Message);
        }

        [Fact]
        public async Task UpdateCategory_Should_Update_Category_Successfully()
        {
            // Arrange
            var categoryId = Guid.NewGuid();

            // Setup mock data
            var existingCategory = new Category
            {
                Id = categoryId,
                CategoryName = "Existing Category",
                CategoryLevel = CategoryLevel.ParentLevel,
                ParentId = null
            };

            var updatedCategoryDTO = new CategoryUpdateRequestDTO
            {
                Id = categoryId,
                CategoryName = "Updated Category",
                CategoryLevel = CategoryLevel.ParentLevel,
                ParentId = null
            };

            var updatedCategory = new Category
            {
                Id = categoryId,
                CategoryName = "Updated Category",
                CategoryLevel = CategoryLevel.ParentLevel,
                ParentId = null
            };

            // Setup the mock service
            _mockCategoryService.Setup(service => service.GetByConditionAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(existingCategory);

            _mockCategoryService.Setup(service => service.UpdateAsync(It.IsAny<Category>(), It.IsAny<Category>()))
                .ReturnsAsync(updatedCategory);

            // Act
            var result = await _categoryController.UpdateCategory(updatedCategoryDTO);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult?.Value);
            Assert.IsType<CategoryResponseDTO>(okResult.Value);

            var updatedCategoryResponse = okResult.Value as CategoryResponseDTO;
            Assert.NotNull(updatedCategoryResponse);
            Assert.Equal("Updated Category", updatedCategoryResponse?.CategoryName);
        }

        [Theory]
        [InlineData("3d2b8791-eaee-450e-8614-819f2beb5ed9", "e2c2d9b7-18b8-4b21-94b1-3c19efc169ec")]
        public async Task DeleteCategory_Should_HandleCategoryDeletion(string validGuidStr, string invalidGuidStr)
        {
            // Arrange
            Guid validGuid = new Guid(validGuidStr);
            Guid invalidGuid = new Guid(invalidGuidStr);

            // Setup mock data
            var existingCategory = new Category
            {
                Id = validGuid,
                CategoryName = "Existing Category",
                CategoryLevel = CategoryLevel.ParentLevel
            };

            // Setup the mock service for retrieving and deleting categories
            _mockCategoryService.Setup(service => service.GetByConditionAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync((Expression<Func<Category, bool>> predicate) =>
                    predicate.Compile().Invoke(existingCategory) ? existingCategory : null);
            _mockCategoryService.Setup(service => service.DeleteAsync(It.IsAny<Category>()))
                .ReturnsAsync(true);

            // Act
            var resultValid = await _categoryController.DeleteCategory(validGuid);
            var exceptionInvalid = await Assert.ThrowsAsync<A6NotFoundException>(() => _categoryController.DeleteCategory(invalidGuid));

            // Assert
            Assert.IsType<NoContentResult>(resultValid);
            Assert.Equal("Category does not exist!", exceptionInvalid.Message);

            _mockCategoryService.Verify(service => service.DeleteAsync(existingCategory), Times.Once);
            _mockCategoryService.Verify(service => service.DeleteAsync(It.IsAny<Category>()), Times.Once);
        }
    }
}
