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

namespace Ibags.API.Controllers
{
    /// <summary>
    /// 新闻
    /// </summary>
    public class NewsController : ApiController
    {
        private IbagsDbContext db = new IbagsDbContext();

        // GET api/News
        /// <summary>
        /// 获取新闻
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings()]
        public IEnumerable<News> GetNews()
        {
            return db.NewsSet.AsEnumerable();
        }

        // GET api/News/5
        public News GetNews(int id)
        {
            News news = db.NewsSet.Find(id);
            if (news == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return news;
        }

        // PUT api/News/5
        public HttpResponseMessage PutNews(int id, News news)
        {
            if (ModelState.IsValid && id == news.NewsId)
            {
                db.Entry(news).State = EntityState.Modified;

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

        // POST api/News
        public HttpResponseMessage PostNews(News news)
        {
            if (ModelState.IsValid)
            {
                db.NewsSet.Add(news);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, news);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = news.NewsId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/News/5
        public HttpResponseMessage DeleteNews(int id)
        {
            News news = db.NewsSet.Find(id);
            if (news == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.NewsSet.Remove(news);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, news);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}