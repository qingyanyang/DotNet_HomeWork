using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NET3Assignment.DTOs;
using NET3Assignment.Models;
using BCrypt.Net;
using System;
using Microsoft.AspNetCore.Http.HttpResults;

namespace NET3Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly IMapper _mapper;

        private static List<User> _userList = new List<User>();

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
            // samples
            if (_userList.Count == 0)
            {
                _userList.Add(new User() { UserId = Guid.NewGuid(), UserName = "Alan", Email = "Alan@example.com", HashedPassword = "hashed password example01" });
                _userList.Add(new User() { UserId = Guid.NewGuid(), UserName = "Mary", Email = "Mary@example.com", HashedPassword = "hashed password example02" });
            }
        }

        [HttpGet]
        public JsonResult GetUserInfo()
        {
            var userDTOs = _mapper.Map<List<UserResponseDTO>>(_userList);
            return new JsonResult(userDTOs);
        }

        [HttpGet("{id:guid}")]
        public JsonResult GetUserInfoById(Guid id)
        {
            // search by id
            var user = _userList.FirstOrDefault(user => user.UserId == id);
            if (user == null)
            {
                return new JsonResult("User not found") { StatusCode = 404 };
            }
            var userDTO = _mapper.Map<UserResponseDTO>(user);
            return new JsonResult(userDTO);
        }

        [HttpGet]
        public JsonResult GetUserInfoByEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return new JsonResult("Email parameter is required") { StatusCode = 400 };
            }
            // search by email
            var user = _userList.FirstOrDefault(user => user.Email == email);
            if (user == null)
            {
                return new JsonResult("User not found") { StatusCode = 404 };
            }
            var userDTO = _mapper.Map<UserResponseDTO>(user);
            return new JsonResult(userDTO);
        }

        [HttpPost]
        public JsonResult CreateUser([FromForm] UserRequestDTO userInfo)
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

            return new JsonResult(userResponseDTO) { StatusCode = 201 };
        }

        [HttpPut("{id:guid}")]
        public JsonResult UpdateUserInfoById([FromBody] UserRequestDTO userInfo, Guid id)
        {
            //search
            var userFound = _userList.FirstOrDefault(user => user.UserId == id);
            if (userFound == null)
            {
                return new JsonResult("User not found!") { StatusCode = 404 };
            }
            //hash password before updating
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userInfo.Password);
            userInfo.Password = hashedPassword;
            //update
            _mapper.Map(userInfo, userFound);

            UserResponseDTO userResponseDTO = _mapper.Map<UserResponseDTO>(userFound);

            return new JsonResult(userResponseDTO);
        }

        [HttpDelete("{id:guid}")]
        public JsonResult DeleteUserById(Guid id)
        {
            var userFound = _userList.FirstOrDefault(user => user.UserId == id);
            if (userFound == null)
            {
                return new JsonResult("User not found!") { StatusCode = 404 };
            }
            _userList.Remove(userFound);

            return new JsonResult("") { StatusCode = 204 };
        }
    }
}