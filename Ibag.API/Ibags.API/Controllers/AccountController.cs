using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using Ibags.API.App_Start;
using Ibags.API.Models;
using System.Net.Http;
using System.Net;

namespace Ibags.API.Controllers
{
    public class AccountController : ApiController
    {
        private IbagsDbContext db = new IbagsDbContext();

        [System.Web.Http.HttpGet]
        public Status Login(string userId, String password)
        {
            //if (String.IsNullOrEmpty(userId))
            //    throw new HttpResponseException(new HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized, Content = new StringContent("Please provide the credentials.") });
            var pwd = Extensions.MD5Hash(password);
            bool isValidUser = db.ed_user.Any(u => u.UserId == userId && u.Password == pwd);
            if (isValidUser)
            {
                Token token = new Token(userId, Request.GetClientIP());
                return new Status { Successeded = true, Token = token.Encrypt(), Message = "Successfully signed in." };
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized, Content = new StringContent("Invalid user name or password.") });
            }
        }

    }
}
