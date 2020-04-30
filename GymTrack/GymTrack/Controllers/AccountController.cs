using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GymTrack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        ApplicationContext appContext;
        List<Person> people;
        public AccountController(ApplicationContext context)
        {
            appContext = context;
            if (!appContext.Users.Any())
            {
                appContext.Users.Add(new Person() { Login = "lviv.home31@gmail.com", Password = "gymtrack1qaz!QAZ", Role = "admin" });
                appContext.SaveChanges();
            }
            people = appContext.Users.ToList();

        }

        [HttpGet]
        public IEnumerable<String> Get()
        {
            return new List<String>() { "Str1", "Val2", "Text3" };
        }

        [HttpPut]
        public IActionResult Put([FromBody]AuthModel model)
        {
            if(appContext.Users.FirstOrDefault(x=> x.Login == model.username) != null)
                return BadRequest(new { errorText = "User with this name is already exists!" });
            else
            {
                try
                {
                    appContext.Users.Add(new Person() { Login = model.username, Password = model.password, Role = "user" });
                    appContext.SaveChanges();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(new { errorText = ex.Message }); 
                }
              
            }
        }
        public class AuthModel
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        [HttpPost]
        public IActionResult Post([FromBody]AuthModel model )
        {
            string username = model.username;
            string password = model.password;
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest("Invalid username or password.");
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                time = AuthOptions.LIFETIME
            };

            return new ObjectResult(response);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            Person person = people.FirstOrDefault(x => x.Login == username && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
