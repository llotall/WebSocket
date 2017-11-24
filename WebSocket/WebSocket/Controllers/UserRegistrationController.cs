using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;
using BusinessLogic.Interfaces.Base.CRUD;
using Shared.Entities.JsonModels;

namespace WebSocket.Controllers
{
    public class UserRegistrationController : BaseController
    {

        public IUserService UserService { get; set; }

        public UserRegistrationController([FromServices]
            IUserService userService)
        {
            UserService = userService;
        }
        [HttpPost("registr")]
        public IActionResult Post(UserRegJsonModel user)
        {
            if (user == null)
                return BadRequest(new { message = "Пустое тело запроса" });

            if (string.IsNullOrEmpty(user.Login))
                return BadRequest(new { message = "Логин не может быть пустым"});

            if (string.IsNullOrEmpty(user.Password))
                return BadRequest(new { message = "Пароль не может быть пустым" });

            if (string.IsNullOrEmpty(user.Name))
                return BadRequest(new { message = "Имя не может быть пустым" });

            var isUserExist = UserService.GetAll().Any(x => x.Login == user.Login);
            if (isUserExist == false)
                UserService.Create(user);

            return BadRequest(new { Result = "ok"});
        }
    }
}
