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
    public class OrderController : ApiController
    {
        private IbagsDbContext db = new IbagsDbContext();

        // GET api/Order
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> GetOrders()
        {
            String accountId = TokenInspector.GetToken(Request).AccountId;
            return db.OrderSet.Where(o => o.AccountId == accountId);
        }

        // GET api/Order/XLGJ000001
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="orderNo">eg. XLGJ000001</param>
        /// <returns></returns>
        public Order GetOrder(string orderNo)
        {
            Order order = db.OrderSet.SingleOrDefault(o => o.OrderNo == orderNo);
            if (order == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return order;
        }

        // PUT api/Order/XLGJ000001
        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public HttpResponseMessage PutOrder(string orderNo, Order order)
        {
            if (ModelState.IsValid && orderNo == order.OrderNo)
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
                // validate accountId
                if (order.AccountId != TokenInspector.GetToken(Request).AccountId)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                order.OrderNo = MakeOrderNo();
                db.OrderSet.Add(order);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, order);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { orderNo = order.OrderNo }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        private string MakeOrderNo()
        {
            Random r = new Random();
            var a = r.Next(1000, 9999);
            var orderNo = a + DateTime.Now.ToString("yyMMddhhmmss");    //140401192504  2014-04-01 19:25:04 3507 140401192504
            return orderNo;
        }

        // DELETE api/Order/5
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage DeleteOrder(string orderNo)
        {
            Order order = db.OrderSet.SingleOrDefault(o => o.OrderNo == orderNo);
            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.OrderSet.Remove(order);

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