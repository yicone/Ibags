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

namespace Ibags.API.Controllers
{
    public class NewsController : ApiController
    {
        private IbagsDbContext db = new IbagsDbContext();

        // GET api/News
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<News> GetNews()
        {
            Logger.Instance().Info("getNews");
            var newsSet = db.NewsSet.AsEnumerable();
            Logger.Instance().Info("getNews count: " + newsSet.Count());
            return newsSet;
        }

        // GET api/News/5
        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        [ApiExplorerSettings(IgnoreApi = true)]
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
        [ApiExplorerSettings(IgnoreApi = true)]
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
        [ApiExplorerSettings(IgnoreApi = true)]
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