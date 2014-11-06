using Scholarship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Scholarship.Controllers
{
    public class AccountController : ApiController
    {
        [Route("api/login")]
        public IHttpActionResult Login(UserModel user)
        {
            UserDatabase udb = new UserDatabase();
            user = udb.ValidUser(user);
            if (user.accessToken.Equals(""))
                return Unauthorized();
            else
                return Ok(user);
        }

        [Route("api/register")]
        public IHttpActionResult Register(UserModel user)
        {
            UserDatabase udb = new UserDatabase();
            string message= udb.RegisterUser(user);
            if (message.Equals(""))
                return Ok(user);
            else
                return NotFound();
        }

    }
}
