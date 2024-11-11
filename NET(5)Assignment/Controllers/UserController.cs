using Microsoft.AspNetCore.Mvc;
using NET_5_Assignment.IService;
using NET_5_Assignment.Models;
using NET_5_Assignment.Service;

namespace NET_5_Assignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult GetUserById(UserCreateInput user)
        {
            bool isCreateSuccess = _userService.Insert(user);
            return Created(string.Empty,201);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id) {
            UserResponse userResponse = _userService.Search(id);
            return Ok(userResponse);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserById([FromBody]UserUpdateInput user, int id)
        {
            bool isUpdateSuccess = _userService.Update(user,id);
            return Ok("update successfully!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserById(int id)
        {
            bool isUpdateSuccess = _userService.Delete(id);
            return NoContent();
        }

    }
}
