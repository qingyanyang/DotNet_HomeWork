
using Net_6_Assignment.Data;
using Net_6_Assignment.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Net_6_Assignment.Services
{
    public class CourseService : IService<Course>
    {
        private readonly A6DbContext _A6DbContext;

        public CourseService(A6DbContext a6DbContext)
        {
            _A6DbContext = a6DbContext;
        }

        public async Task<Course> CreateAsync(Course Course)
        {
            // create a new Guid id
            Course.Id = Guid.NewGuid();
            await _A6DbContext.DBCourse.AddAsync(Course);
            await _A6DbContext.SaveChangesAsync();

            return Course;
        }

        public async Task<bool> DeleteAsync(Course Course)
        {
            _A6DbContext.DBCourse.Remove(Course);
            return await _A6DbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            IEnumerable<Course> CourseList = await _A6DbContext.DBCourse
                .Include(c => c.Category)
                .Include(c => c.User)
                .ToListAsync();
            return CourseList;
        }

        public async Task<Course?> GetByConditionAsync(Expression<Func<Course, bool>> predicate)
        {
            var existingCourse = await _A6DbContext.DBCourse
                .Include(c => c.Category)
                .Include(c => c.User)
                .FirstOrDefaultAsync(predicate);
            return existingCourse;
        }

        public async Task<Course> UpdateAsync(Course existingCourse, Course updatedCourse)
        {
            existingCourse.CourseName = updatedCourse.CourseName;
            existingCourse.Description = updatedCourse.Description;
            existingCourse.CategoryId = updatedCourse.CategoryId;

            await _A6DbContext.SaveChangesAsync();

            return existingCourse;
        }
    }
}
