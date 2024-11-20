using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Net_6_Assignment.Data;
using Net_6_Assignment.Models;


namespace Net_6_Assignment.Services
{
    public class UserService : IService<User>
    {
        private readonly A6DbContext _A6DbContext;

        public UserService(A6DbContext a6DbContext)
        {
            _A6DbContext = a6DbContext;
        }

        public async Task<User> CreateAsync(User User)
        {
            await _A6DbContext.DBUser.AddAsync(User);
            await _A6DbContext.SaveChangesAsync();

            return User;
        }

        public async Task<bool> DeleteAsync(User existingUser)
        {
            _A6DbContext.DBUser.Remove(existingUser);
             await _A6DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var UserList = await _A6DbContext.DBUser.ToListAsync();
            return UserList;
        }

        public async Task<User?> GetByConditionAsync(Expression<Func<User, bool>> predicate)
        {
            var existingUser = await _A6DbContext.DBUser
                .Include(u=>u.Courses)
                .FirstOrDefaultAsync(predicate);
            return existingUser;
        }

        public async Task<User> UpdateAsync(User existingUser, User updatedUser)
        {
            // update only non-null fields from DTO
            if (!string.IsNullOrEmpty(updatedUser.UserName))
            {
                existingUser.UserName = updatedUser.UserName;
            }

            if (!string.IsNullOrEmpty(updatedUser.Email))
            {
                existingUser.Email = updatedUser.Email;
            }

            if (!string.IsNullOrEmpty(updatedUser.Address))
            {
                existingUser.Address = updatedUser.Address;
            }

            if (updatedUser.Gender.HasValue)
            {
                existingUser.Gender = updatedUser.Gender.Value;
            }

            if (!string.IsNullOrEmpty(updatedUser.Phone))
            {
                existingUser.Phone = updatedUser.Phone;
            }

            await _A6DbContext.SaveChangesAsync();

            return existingUser;
        }
    }
}
