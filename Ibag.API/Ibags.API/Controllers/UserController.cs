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
    public class UserController : ApiController
    {
        private IbagsDbContext db = new IbagsDbContext();

        // GET api/User
        [ApiExplorerSettings(IgnoreApi = true)]
        public IEnumerable<User> GetUsers()
        {
            return db.ed_user.AsEnumerable();
        }

        // GET api/User/5
        /// <summary>
        /// 获取帐号信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUser(int id)
        {
            User user = db.ed_user.Find(id);
            if (user == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return user;
        }

        // PUT api/User/5
        /// <summary>
        /// 修改帐号信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public HttpResponseMessage PutUser(String id, User user)
        {
            if (ModelState.IsValid && id == user.UserId)
            {
                db.Entry(user).State = EntityState.Modified;

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

        // POST api/User
        /// <summary>
        /// 注册帐号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public HttpResponseMessage PostUser(User user)
        {
            if (ModelState.IsValid)
            {
                db.ed_user.Add(user);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.UserId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/User/5
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage DeleteUser(int id)
        {
            User user = db.ed_user.Find(id);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ed_user.Remove(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}