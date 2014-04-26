using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Ibags.API.App_Start;
using Ibags.API.Models;

namespace Ibags.API.Controllers
{
    public class AccountController : ApiController
    {
        private IbagsDbContext db = new IbagsDbContext();

        // GET api/Account
        [ApiExplorerSettings(IgnoreApi = true)]
        public IEnumerable<Account> GetAccounts()
        {
            return db.AccountSet.AsEnumerable();
        }

        // GET api/Account/
        /// <summary>
        /// 获取帐号信息
        /// </summary>
        /// <returns></returns>
        public Account GetAccount()
        {
            Account account = db.AccountSet.SingleOrDefault(u => u.AccountId == TokenInspector.GetToken(Request).AccountId);
            if (account == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return account;
        }

        // POST api/Account
        /// <summary>
        /// 注册帐号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public HttpResponseMessage PostAccount(Account account)
        {
            if (ModelState.IsValid)
            {
                db.AccountSet.Add(account);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, account);
                response.Headers.Location = new Uri(Url.Link("Registration", new { id = account.AccountId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/Account/5
        /// <summary>
        /// 修改帐号信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public HttpResponseMessage PutAccount(Account account)
        {
            string id = TokenInspector.GetToken(Request).AccountId;
            if (ModelState.IsValid && id == account.AccountId)
            {
                db.Entry(account).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Account/5
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage DeleteAccount(int id)
        {
            Account account = db.AccountSet.Find(id);
            if (account == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.AccountSet.Remove(account);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, account);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}