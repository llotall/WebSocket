using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Masterslavl.BusinessLogic.Interfaces.Base.CRUD;
using Masterslavl.Models;
using Masterslavl.Auth;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Masterslavl.Shared.JsonModels;
using Masterslavl.Utils;
using Masterslavl.Controllers;
using Masterslavl.Shared.Entities;

namespace Masterslavl.Controllers
{
    [Route("api/[controller]")]
    public class TokenAuthController : BaseController
    {
        public IAdminService AdminService;
        public ISessionAdminService SessionAdminService;

        public TokenAuthController(
            [FromServices] IAdminService adminService,
            [FromServices] ISessionAdminService sessionAdminService)
        {
            AdminService = adminService;
            SessionAdminService = sessionAdminService;
        }

        [HttpPost("get_auth_token")]
        [AllowAnonymous]
        public string GetAuthToken([FromBody]AdminJson user)
        {
            var existAdmin = AdminService.GetAll().FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);

            var currentDate = DateTime.Now;

            if (existAdmin != null)
            {
                var requestAt = currentDate;
                var expiresIn = requestAt + TokenAuthOption.ExpiresSpan;
                var token = GenerateToken(existAdmin, expiresIn);

                return JsonConvert.SerializeObject(new RequestResult
                {
                    State = RequestState.Success,
                    Data = new
                    {
                        requertAt = requestAt,
                        expiresIn = TokenAuthOption.ExpiresSpan.TotalSeconds,
                        tokeyType = TokenAuthOption.TokenType,
                        accessToken = token
                    }
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

        private string GenerateToken(Admin admin, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim("Id", admin.ID.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, admin.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, admin.Role)
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
