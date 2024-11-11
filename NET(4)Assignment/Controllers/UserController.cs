using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NET3Assignment.DTOs;
using NET3Assignment.Models;
using NET3Assignment.Common;
using NET3Assignment.Common.Exceptions;

namespace NET3Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly IMapper _mapper;

        private static List<User> _userList = new List<User>();

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
            // samples
            if (_userList.Count == 0)
            {
                _userList.Add(new User() { UserId = Guid.NewGuid(), UserName = "Alan", Email = "Alan@example.com", HashedPassword = "hashed password example01", Role =0, Gender = 0, Address = "60 Montacute Rd", Phone = "+61 458356727" });
                _userList.Add(new User() { UserId = Guid.NewGuid(), UserName = "Mary", Email = "Mary@example.com", HashedPassword = "hashed password example02", Role = 0, Gender = 0, Address = "60 Montacute Rd", Phone = "+61 458356727" });
            }
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetUserById(Guid id)
        {
            // search by id
            // not found
            var user = _userList.FirstOrDefault(user => user.UserId == id); 
            if (user == null)
            {
                throw new A4NotFoundException("user not found.");
            }
            // found
            var userDTO = _mapper.Map<UserResponseDTO>(user);

            return Ok(userDTO);
        }

        [HttpGet]
        public IActionResult GetUser([FromQuery] string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                var userDTOs = _mapper.Map<List<UserResponseDTO>>(_userList);

                return Ok(userDTOs);
            }
            // Search by email
            var user = _userList.FirstOrDefault(user => user.Email == email);
            if (user == null)
            {
                throw new A4NotFoundException("user not found.");
            }

            // Map user to DTO and return success response
            var userDTO = _mapper.Map<UserResponseDTO>(user);

            return Ok(userDTO);
        }

        [HttpPost]
        public ActionResult<CommonResponse<UserRequestDTO>> CreateUser([FromForm] UserRequestDTO userInfo)
        {
            // get raw password
            string rawPassword = userInfo.Password;
            // hash password and save to user
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(rawPassword);
            userInfo.Password = hashedPassword;
            // save to db
            User newUser = _mapper.Map<User>(userInfo);
            newUser.UserId = Guid.NewGuid();
            _userList.Add(newUser);
            // response to client side
            UserResponseDTO userResponseDTO = _mapper.Map<UserResponseDTO>(newUser);

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserId }, userResponseDTO);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateUserInfoById([FromBody] UserRequestDTO userInfo, Guid id)
        {
            //search
            var userFound = _userList.FirstOrDefault(user => user.UserId == id);
            if (userFound == null)
            {
                throw new A4NotFoundException("user not found.");
            }

            //hash password before updating
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userInfo.Password);
            userInfo.Password = hashedPassword;
            //update
            _mapper.Map(userInfo, userFound);

            UserResponseDTO userResponseDTO = _mapper.Map<UserResponseDTO>(userFound);

            return Ok(userResponseDTO);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult DeleteUserById(Guid id)
        {
            var userFound = _userList.FirstOrDefault(user => user.UserId == id);
            if (userFound == null)
            {
                throw new A4NotFoundException("user not found.");
            }

            _userList.Remove(userFound);

            return NoContent();
        }
    }
}