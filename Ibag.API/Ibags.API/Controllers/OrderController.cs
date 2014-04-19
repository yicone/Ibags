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
    public class OrderController : ApiController
    {
        private IbagsDbContext db = new IbagsDbContext();

        // GET api/Order
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        public IEnumerable<Order> GetOrders(string userNo)
        {
            return db.ed_order.Where(o => o.UserNo == userNo);
        }

        // GET api/Order/5
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order GetOrder(int id)
        {
            Order order = db.ed_order.Find(id);
            if (order == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return order;
        }

        // PUT api/Order/5
        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public HttpResponseMessage PutOrder(int id, Order order)
        {
            if (ModelState.IsValid && id == order.OrderId)
            {
                db.Entry(order).State = EntityState.Modified;

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

        // POST api/Order
        /// <summary>
        /// 新建订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public HttpResponseMessage PostOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                db.ed_order.Add(order);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, order);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = order.OrderId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Order/5
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage DeleteOrder(int id)
        {
            Order order = db.ed_order.Find(id);
            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ed_order.Remove(order);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, order);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}