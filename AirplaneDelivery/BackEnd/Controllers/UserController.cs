using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using DAL.Models;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private DatabaseContext db;
        [HttpPost("SignUp")]
        public ActionResult<User> SignUp(string login, string password)
        {
            var _user = db.Users.FirstOrDefault(x => x.Name == login);
            if (_user == null)
            {
                var newUser = new User { Name = login, Password = password };
                db.Users.Add(newUser);
                db.SaveChanges();
                return Ok(newUser);
            }
            return BadRequest("Пользователь с такими данными уже зарегистрирован");
        }

        [HttpGet("SignIn")]
        public ActionResult<User> SignIn(string login, string password)
        {
            var _user = db.Users.FirstOrDefault(x => x.Name == login && x.Password == password);
            if (_user != null)
                return (Ok(_user));
            return BadRequest("Пользователь не найден");
        }
    }
}
