using DataAL.Entities.Models;
using DataAL.Entities.UserDbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using UserManagement.BLL.Models.Users;

namespace UserManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        public static User user = new User();

        private readonly IUserService _userService;

        private readonly IConfiguration _configuration;
        public AuthorizationController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult Registr(UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (_userService.UserExists(userDto.PrivateNumber.Trim()))
            {
                return Ok(new { message = "User-ი ამ -'" + userDto.PrivateNumber + "'-ით უკვე არსებობს" });
            }

            _userService.AddUser(userDto);
            return Ok(new { message = "User created" });
        }


        [HttpPost("login")]
        public ActionResult Registr(UserLoginDto userLoginDto)
        {
            var privateNumber = userLoginDto.PrivateNumber;
           user = _userService.GetUserByPrivateNumber(privateNumber);
            string pass = "";

            if (!_userService.UserExists(privateNumber.Trim()))
            {
                return Ok(new { message = "User-ი ამ -'" + privateNumber + "'-ით არ არსებობს" });
            }

            pass = _userService.CreatePasswordHash(userLoginDto.Password);

            if (user.PasswordHash != pass)
            {
                return BadRequest("Wrong password");
            }

            TokenResponse token = new TokenResponse();

            var refreshToken = GenerateRefreshToken();

            setRefreshToken(refreshToken);

            token.JWTToken = CreateToken(user);

            token.RefreshToken = refreshToken.Token;

            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public ActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");

            }

            TokenResponse token = new TokenResponse();

            var newrefreshken = GenerateRefreshToken();

            setRefreshToken(newrefreshken);

            token.JWTToken = CreateToken(user);

            token.RefreshToken = newrefreshken.Token;

            return Ok(token);
        }


        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };
            return refreshToken;

        }

        private void setRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };

            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;

        }

        private string CreateToken(User user)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.Role,((Role) Enum.ToObject(typeof (Role), user.Role)).ToString()),
                new Claim("role",((Role) Enum.ToObject(typeof (Role), user.Role)).ToString())
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now,
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
