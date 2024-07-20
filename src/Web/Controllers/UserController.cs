//using Application.Interfaces;
//using Application.Models.Request;
//using Domain.Entities;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Web.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly IUserService _userservice;

//        public UserController(IUserService userService)
//        {
//            _userservice = userService;
//        }

//        [HttpGet]
//        public IActionResult GetAll() 
//        {
//            return Ok(_userservice.GetAll());
//        }

//        [HttpGet("{id}")]
//        public IActionResult Get([FromRoute] int id)
//        {
//            return Ok(_userservice.GetById(id));
//        }

//        [HttpGet("username/{username}")]
//        public IActionResult GetUserByUserName([FromRoute]string userName)
//        {
//            return Ok(_userservice.GetUserByUserName(userName)) ;
//        }

//        [HttpPost]
//        public IActionResult Add([FromBody] UserCreateRequest userCreateRequest)
//        {
//            return Ok(_userservice.AddUser(userCreateRequest));
//        }


//    }
//}
