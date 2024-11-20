#pragma warning restore CS1591
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net_6_Assignment.Common.Enums;
using Net_6_Assignment.Common.Exceptions;
using Net_6_Assignment.DTOs;
using Net_6_Assignment.Models;
using Net_6_Assignment.Service;
using Net_6_Assignment.Services;


namespace Net_6_Assignment.Controllers
{
    [ApiExplorerSettings(GroupName = nameof(APIVersion.V1))]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly CreateTokenService _createTokenService;
        private readonly IService<User> _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginController> _logger;

        public LoginController(CreateTokenService createTokenService, IService<User> userService, IMapper mapper, ILogger<LoginController> logger)
        {
            _createTokenService = createTokenService;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Authenticates a user based on their email and password, and generates an access token upon successful authentication.
        /// </summary>
        /// <param name="loginInput">The login request DTO containing the user's email and password.</param>
        /// <returns>A UserResponseDTO containing the authenticated user's information and an access token if successful.</returns>
        /// <exception cref="A6NotFoundException">Thrown when the user with the provided email is not found.</exception>
        /// <exception cref="A6UnauthorizedException">Thrown when the provided password does not match the user's stored password.</exception>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDTO?>> LoginAsync([FromBody] UserLoginRequestDTO loginInput)
        {
            _logger.LogInformation("Attempting to authenticate user with email: {Email}", loginInput.Email);

            // Get user by email
            User? existingUser = await _userService.GetByConditionAsync(user => user.Email == loginInput.Email);
            if (existingUser == null)
            {
                _logger.LogWarning("Authentication failed. User with email: {Email} does not exist.", loginInput.Email);
                throw new A6NotFoundException("User does not exist!");
            }

            // Compare password
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(loginInput.Password, existingUser.HashedPassword);
            if (!isPasswordCorrect)
            {
                _logger.LogWarning("Authentication failed for user with email: {Email}. Incorrect password.", loginInput.Email);
                throw new A6UnauthorizedException("Password is not correct!");
            }

            // Password correct, create token
            _logger.LogInformation("Password verified for user with email: {Email}. Generating access token.", loginInput.Email);
            var claims = new Dictionary<string, string>
            {
                { "UserId", existingUser.Id.ToString() },
                { "UserName", existingUser.UserName }
            };

            var accessToken = _createTokenService.CreateToken(claims);
            _logger.LogInformation("Access token generated for user with email: {Email}", loginInput.Email);

            // Return user info with token
            UserResponseDTO userLoginInfo = _mapper.Map<UserResponseDTO>(existingUser);
            userLoginInfo.AccessToken = accessToken;

            _logger.LogInformation("User with email: {Email} successfully authenticated.", loginInput.Email);
            return Ok(userLoginInfo);
        }
    }
}