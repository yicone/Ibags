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
    public class TokenController : ApiController
    {
        private IbagsDbContext db = new IbagsDbContext();

        /// <summary>
        /// get an access token
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <param name="password">客户端MD5后的结果</param>
        /// <returns></returns>
        public string GetToken(string mobileNo, string password)
        {
            //if (String.IsNullOrEmpty(userId))
            //    throw new HttpResponseException(new HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized, Content = new StringContent("Please provide the credentials.") });

            Account user = db.AccountSet.SingleOrDefault(u => u.MobileNo == mobileNo && u.Password == password);
            if (user != null)
            {
                Token token = new Token(user.AccountId, Request.GetClientIP());
                return token.Encrypt();
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized, Content = new StringContent("Invalid user name or password.") });
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
