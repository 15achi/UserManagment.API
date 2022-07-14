

using DataAL.Entities.UserDbModel;
using DataAL.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserManagement.BLL.Models.Users;

namespace UserManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase 
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        [Route("GetUsers")]
       // [Authorize(Roles = "Admin,User")]
        public IActionResult GetUsers([FromQuery] UserParameters userParameters)
            {
                var users = _userService.GetUsers(userParameters);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var metadata = new
                {
                    users.TotalCount,
                    users.PageSize,
                    users.CurrentPage,
                    users.hasNext,
                    users.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(users);
            }
        


        [HttpPost]
       // [Route("CreateUser")]
      //  [Authorize(Roles = "Admin")]
        public IActionResult CreateUser([FromBody] UserDto userDto)
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

        [HttpGet("{PrivateNumber}")]
      //  [Authorize(Roles = "Admin,User")]
        public IActionResult GetUserByPrivateNumber(string PrivateNumber)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userService.UserExists(PrivateNumber.Trim()))
            {
                return Ok(new { message = "User-ი ამ -'" + PrivateNumber + "'-ით არ არსებობს" });
            }

            var user = _userService.UsersGetDtoByPrivateNumber(PrivateNumber);
            return Ok(user);

        }

        [HttpPut]
       // [Route("UpdateUserByPrivateNumber")]
      //  [Authorize(Roles = "Admin")]
        public IActionResult EditUser([FromBody] UserEditDto userEditDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userService.EditUser(userEditDto.PrivateNumber, userEditDto);
            return Ok(new { message = "User Update" });

        }

        [HttpDelete("{privatenumber}")]
    //    [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(string privatenumber)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userService.DeleteUser(privatenumber);
            return Ok(new { message = "User deleted" }); 
        }

        [HttpPut("PassingTheUser/{privatenumber}")]
     //   [Authorize(Roles = "Admin")]
        public IActionResult PassiveOfUser(string privatenumber)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userService.PassingTheUser(privatenumber);
            return Ok(new { message = "user passed" }); 
        }

        [HttpPut("ActivationOfUser/{privatenumber}")]
     //   [Authorize(Roles = "Admin")]
        public IActionResult ActivationOfUser(string privatenumber)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userService.ActivationOfUser(privatenumber);
            return Ok(new { message = "user Activated" });
        }
    }
}
