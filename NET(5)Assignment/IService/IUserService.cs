using NET_5_Assignment.Models;

namespace NET_5_Assignment.IService
{
    public interface IUserService
    {
        public bool Insert(UserCreateInput user);
        public bool Update(UserUpdateInput user,int id);
        public bool Delete(int id);
        public UserResponse Search(int id);
    }
}
