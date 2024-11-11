
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using Net_6_Assignment.Data;
//using Net_6_Assignment.Models;
//using Net_6_Assignment.Services;
//using System.Data.Entity.Infrastructure;
//using TestingDemo;


//namespace Net_6_Assignment.Test.Service.Test
//{
//    public class CourseServiceMockTest
//    {
//        private readonly IService<Course> _service;
//        private readonly IQueryable<Course> _data;
//        private readonly Mock<DbSet<Course>> _mockDBCourse;
//        private readonly Mock<A6DbContext> _mockA6DbContext;
//        public CourseServiceMockTest()
//        {
//            // set up
//            _data = new List<Course>
//            {
//                new Course { Id = Guid.NewGuid(), CourseName = "Course A", Description = "Description A", CategoryId = Guid.NewGuid(), UserId = Guid.NewGuid() },
//                new Course { Id = Guid.NewGuid(), CourseName = "Course B", Description = "Description B", CategoryId = Guid.NewGuid(), UserId = Guid.NewGuid() },
//                new Course { Id = Guid.NewGuid(), CourseName = "Course C", Description = "Description C", CategoryId = Guid.NewGuid(), UserId = Guid.NewGuid() },
//            }.AsQueryable();

//            _mockDBCourse = new Mock<DbSet<Course>>();
//            _mockDBCourse.As<IDbAsyncEnumerable<Course>>()
//            .Setup(m => m.GetAsyncEnumerator())
//            .Returns(new TestDbAsyncEnumerator<Course>(_data.GetEnumerator()));
//            _mockDBCourse.As<IQueryable<Course>>()
//            .Setup(m => m.Provider)
//            .Returns(new TestDbAsyncQueryProvider<Course>(_data.Provider));

//            _mockDBCourse.As<IQueryable<Course>>().Setup(m => m.Expression).Returns(_data.Expression);
//            _mockDBCourse.As<IQueryable<Course>>().Setup(m => m.ElementType).Returns(_data.ElementType);
//            _mockDBCourse.As<IQueryable<Course>>().Setup(m => m.GetEnumerator()).Returns(() => _data.GetEnumerator());

//            _mockA6DbContext = new Mock<A6DbContext>();
//            _mockA6DbContext.Setup(c => c.DBCourse).Returns(_mockDBCourse.Object);

//            _service = new CourseService(_mockA6DbContext.Object);
//        }

//        [Fact]
//        public async Task GetAllAsync_ShouldReturnAllCourses()
//        {
//            // Act
//            var courses = await _service.GetAllAsync() as List<Course>;

//            // Assert
//            Assert.Equal(3, courses?.Count());
//            Assert.Equal("Course A", courses?[0].CourseName);
//            Assert.Equal("Course B", courses?[1].CourseName);
//            Assert.Equal("Course C", courses?[2].CourseName);
//        }

//        [Fact]
//        public async Task GetByIdAsync_ShouldReturnCourseById()
//        {
//            // Arrange
//            var validGuid1 = _data.ElementAt(0).Id;
//            var validGuid2 = _data.ElementAt(1).Id;
//            var validGuid3 = _data.ElementAt(2).Id; // Get a valid ID from the data
//            var invalidGuid = Guid.NewGuid();

//            //Act
//            var course1 = await _service.GetByConditionAsync(c => c.Id == validGuid1);
//            var course2 = await _service.GetByConditionAsync(c => c.Id == validGuid2);
//            var course3 = await _service.GetByConditionAsync(c => c.Id == validGuid3);
//            var notFound = await _service.GetByConditionAsync(c => c.Id == invalidGuid);

//            //Assert
//            Assert.NotNull(course1);
//            Assert.NotNull(course2);
//            Assert.NotNull(course3);
//            Assert.Null(notFound);

//            Assert.Equal("Course A", course1?.CourseName);
//            Assert.Equal("Course B", course2?.CourseName);
//            Assert.Equal("Course C", course3?.CourseName);
//        }

//        [Fact]
//        public async Task CreateAsync_ShouldCreateCourse()
//        {
//            // Arrange
//            var newCourse = new Course
//            {
//                CourseName = "New Course",
//                Description = "New Description",
//                CategoryId = Guid.NewGuid(),
//                UserId = Guid.NewGuid()
//            };

//            //Act
//            var result = await _service.CreateAsync(newCourse);

//            //Assert
//            Assert.NotNull(result);
//            Assert.Equal("New Course", result.CourseName);
//            Assert.Equal("New Description", result.Description);
//            Assert.NotEqual(Guid.Empty, result.Id);
//            _mockDBCourse.Verify(m => m.AddAsync(It.IsAny<Course>(), default), Times.Once());
//            _mockA6DbContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
//        }

//        [Fact]
//        public async Task DeleteAsync_ShoudDeleteCourse()
//        {
//            //Arrange
//            var courseToDelete = _data.ElementAt(0);

//            //Act
//            var result = await _service.DeleteAsync(courseToDelete);

//            //Assert
//            _mockDBCourse.Verify(m => m.Remove(It.IsAny<Course>()), Times.Once());
//            _mockA6DbContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
//        }

//        [Fact]
//        public async Task UpdateAsync_ShoudUpdateCourse()
//        {
//            //Arrange
//            var courseToUpdate = _data.ElementAt(0);
//            var updatedCourse = new Course
//            {
//                Id = courseToUpdate.Id,
//                CourseName = "Updated Course Name",
//                Description = "Updated Course Description",
//                CategoryId = Guid.NewGuid(),
//                UserId = courseToUpdate.UserId
//            };

//            //Act
//            var result = await _service.UpdateAsync(courseToUpdate, updatedCourse);

//            //Assert
//            Assert.Equal("Updated Course Name", result.CourseName);
//            Assert.Equal("Updated Course Description", result.Description);
//            Assert.Equal(updatedCourse.CategoryId, result.CategoryId);

//            _mockA6DbContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
//        }
//    }
//}
