using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebSocketApi.Auth;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Shared.Entities;
using BusinessLogic.Interfaces.Base.CRUD;
using Shared.Entities.JsonModels;

namespace WebSocketApi
{
    [Route("api/[controller]")]
    public class TokenAuthController : BaseController
    {
        public IUserService UserService;
        public IUserSessionService UserSessionService;

        public TokenAuthController(
            [FromServices] IUserService userService,
            [FromServices] IUserSessionService userSessionService
            )
        {
            UserService = userService;
            UserSessionService = userSessionService;
        }

        [HttpPost("get_auth_token")]
        [AllowAnonymous]
        public string GetAuthToken([FromBody]UserJsonModel user)
        {
            var existAdmin = UserService.GetAll().FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);

            var currentDate = DateTime.Now;

            if (existAdmin != null)
            {
                var requestAt = currentDate;
                var expiresIn = requestAt + TokenAuthOption.ExpiresSpan;
                var token = GenerateToken(existAdmin, expiresIn);

                return JsonConvert.SerializeObject(new RequestResult
                {
                    State = RequestState.Success,
                    AccessToken = token
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new RequestResult
                {
                    State = RequestState.Failed,
                    Msg = "Username or password is invalid"
                });
            }
        }

        private string GenerateToken(User admin, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim("Id", admin.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, admin.Login),
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "TokenAuth", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenAuthOption.Issuer,
                Audience = TokenAuthOption.Audience,
                SigningCredentials = TokenAuthOption.SigningCredentials,
                Subject = identity,
                Expires = expires
            });
            return handler.WriteToken(securityToken);
        }
    }
}
