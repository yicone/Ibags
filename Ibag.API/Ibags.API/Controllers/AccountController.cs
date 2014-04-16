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

namespace Ibags.API.Controllers
{
    public class AccountController : ApiController
    {
        private IbagsDbContext db = new IbagsDbContext();

        // GET api/Account
        public IEnumerable<Account> GetAccounts()
        {
            return db.ed_user.AsEnumerable();
        }

        // GET api/Account/5
        public Account GetAccount(int id)
        {
            Account account = db.ed_user.Find(id);
            if (account == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return account;
        }

        // PUT api/Account/5
        public HttpResponseMessage PutAccount(int id, Account account)
        {
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

        // POST api/Account
        public HttpResponseMessage PostAccount(Account account)
        {
            if (ModelState.IsValid)
            {
                db.ed_user.Add(account);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, account);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = account.AccountId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Account/5
        public HttpResponseMessage DeleteAccount(int id)
        {
            Account account = db.ed_user.Find(id);
            if (account == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ed_user.Remove(account);

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