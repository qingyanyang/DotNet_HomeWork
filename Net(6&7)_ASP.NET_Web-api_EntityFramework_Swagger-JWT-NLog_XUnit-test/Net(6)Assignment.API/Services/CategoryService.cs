using Net_6_Assignment.Data;
using Net_6_Assignment.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Net_6_Assignment.Services
{
    public class CategoryService: IService<Category>
    {
        private readonly A6DbContext _A6DbContext;

        public CategoryService(A6DbContext a6DbContext)
        {
            _A6DbContext = a6DbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            // create a new Guid id
            category.Id = Guid.NewGuid();
            await _A6DbContext.DBCategory.AddAsync(category);

            await _A6DbContext.SaveChangesAsync();

            return category;
        }

        public async Task<bool> DeleteAsync(Category category)
        {
            _A6DbContext.DBCategory.Remove(category);
            return await _A6DbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            IEnumerable<Category> categoryList = await _A6DbContext.DBCategory
                .Include(c => c.Courses)// get course list from where has the same category id
                .Include(c => c.Parent)// get parent category
                .ToListAsync();
            return categoryList;
        }

        public async Task<Category?> GetByConditionAsync(Expression<Func<Category, bool>> predicate)
        {
            var existingCategory = await _A6DbContext.DBCategory
                .Include(c => c.Courses)
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(predicate);
            return existingCategory;
        }

        public async Task<Category> UpdateAsync(Category existingCategory, Category updatedCategory)
        {
            existingCategory.CategoryName = updatedCategory.CategoryName;
            existingCategory.CategoryLevel = updatedCategory.CategoryLevel;
            existingCategory.ParentId = updatedCategory.ParentId;

            await _A6DbContext.SaveChangesAsync();

            return existingCategory;
        }
    }
}
