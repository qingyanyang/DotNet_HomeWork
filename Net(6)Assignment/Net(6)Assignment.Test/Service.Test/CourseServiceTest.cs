using Microsoft.EntityFrameworkCore;
using Net_6_Assignment.Common.Enums;
using Net_6_Assignment.Data;
using Net_6_Assignment.Models;
using Net_6_Assignment.Services;

namespace Net_6_Assignment.Test.Service.Test
{
    public class CourseServiceTest
    {
        private readonly A6DbContext _context; // use real context but in mermory
        private readonly IService<Course> _service;
        public CourseServiceTest()
        {
            var options = new DbContextOptionsBuilder<A6DbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new A6DbContext(options);

            _service = new CourseService(_context);

            // Add mock Category data
            var categories = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), CategoryName = "Category 1", CategoryLevel =  CategoryLevel.ParentLevel},
                new Category { Id = Guid.NewGuid(), CategoryName = "Category 2", CategoryLevel = CategoryLevel.ChildLevel }
            };
            _context.DBCategory.AddRange(categories);

            // Add mock User data
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), UserName = "User 1", Email = "user1@example.com",HashedPassword = "SampleHashedPassword01" },
                new User { Id = Guid.NewGuid(), UserName = "User 2", Email = "user2@example.com",HashedPassword = "SampleHashedPassword02" }
            };
            _context.DBUser.AddRange(users);

            // Add mock Course data and associate with Category and User
            _context.DBCourse.AddRange(new List<Course>
            {
                new Course { Id = Guid.NewGuid(), CourseName = "Course A", Description = "Description A", CategoryId = categories[0].Id, UserId = users[0].Id },
                new Course { Id = Guid.NewGuid(), CourseName = "Course B", Description = "Description B", CategoryId = categories[1].Id, UserId = users[1].Id },
                new Course { Id = Guid.NewGuid(), CourseName = "Course C", Description = "Description C", CategoryId = categories[0].Id, UserId = users[0].Id },
            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllCourses()
        {
            // Act
            var courses = await _service.GetAllAsync() as List<Course>;

            // Assert
            Assert.Equal(3, courses?.Count());
            Assert.Equal("Course A", courses?[0].CourseName);
            Assert.Equal("Course B", courses?[1].CourseName);
            Assert.Equal("Course C", courses?[2].CourseName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCourseById()
        {
            // Arrange
            var validGuid1 = _context.DBCourse.First().Id;
            var invalidGuid = Guid.NewGuid();

            //Act
            var course1 = await _service.GetByConditionAsync(c => c.Id == validGuid1);
            var notFound = await _service.GetByConditionAsync(c => c.Id == invalidGuid);

            //Assert
            Assert.NotNull(course1);
            Assert.Null(notFound);

            Assert.Equal("Course A", course1?.CourseName);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateCourse()
        {
            // Arrange
            var newCourse = new Course
            {
                CourseName = "New Course",
                Description = "New Description",
                CategoryId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };
            int countPrev = _context.DBCourse.Count();

            //Act
            var result = await _service.CreateAsync(newCourse);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("New Course", result.CourseName);
            Assert.Equal("New Description", result.Description);
            Assert.NotEqual(Guid.Empty, result.Id);
            
            Assert.Equal(1, _context.DBCourse.Count() - countPrev);
        }
        [Fact]
        public async Task DeleteAsync_ShoudDeleteCourse()
        {
            //Arrange
            var courseToDelete = _context.DBCourse.First();
            int countPrev = _context.DBCourse.Count();

            //Act
            var result = await _service.DeleteAsync(courseToDelete);

            //Assert
            Assert.True(result);
            Assert.Equal(1, countPrev - _context.DBCourse.Count());
        }
        [Fact]
        public async Task UpdateAsync_ShoudUpdateCourse()
        {
            //Arrange
            var courseToUpdate = _context.DBCourse.First();
            var updatedCourse = new Course
            {
                Id = courseToUpdate.Id,
                CourseName = "Updated Course Name",
                Description = "Updated Course Description",
                CategoryId = Guid.NewGuid(),
                UserId = courseToUpdate.UserId
            };

            //Act
            var result = await _service.UpdateAsync(courseToUpdate, updatedCourse);

            //Assert
            Assert.Equal("Updated Course Name", result.CourseName);
            Assert.Equal("Updated Course Description", result.Description);
            Assert.Equal(updatedCourse.CategoryId, result.CategoryId);
            
            Assert.Equal("Updated Course Name", _context.DBCourse.First().CourseName);
            Assert.Equal("Updated Course Description", _context.DBCourse.First().Description);
            Assert.Equal(updatedCourse.CategoryId, _context.DBCourse.First().CategoryId);
        }
    }
}
