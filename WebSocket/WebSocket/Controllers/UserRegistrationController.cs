using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;
using BusinessLogic.Interfaces.Base.CRUD;
using Shared.Entities.JsonModels;
using Microsoft.AspNetCore.Authorization;

namespace WebSocketApi.Controllers
{
    public class UserRegistrationController : BaseController
    {

        public IUserService UserService { get; set; }

        public UserRegistrationController([FromServices]
            IUserService userService)
        {
            UserService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Post([FromBody]UserRegJsonModel user)
        {
            if (user == null)
                return BadRequest(new { msg = "Пустое тело запроса" });

            if (string.IsNullOrEmpty(user.Login))
                return BadRequest(new { msg = "Логин не может быть пустым"});

            if (string.IsNullOrEmpty(user.Password))
                return BadRequest(new { msg = "Пароль не может быть пустым" });

            if (string.IsNullOrEmpty(user.Name))
                return BadRequest(new { msg = "Имя не может быть пустым" });

            var isUserExist = UserService.GetAll().Any(x => x.Login == user.Login);
            if (isUserExist)
                return Json(new { msg = "Логин занят другим пользователем" });
            UserService.Create(user);

            return Json(new { msg = "ok"});
        }
    }
}
