using Ibags.API.App_Start;
using Ibags.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Ibags.API.Controllers
{
    public class MessageController : ApiController
    {
        private IbagsDbContext db = new IbagsDbContext();

        // POST api/Message
        public HttpResponseMessage PostMessage(MessageModel message)
        {
            if (ModelState.IsValid)
            {
                var msg = new Message();
                msg.MobileNo = message.MobileNo;
                msg.MessageType = (int)message.MessageType;
                msg.Content = GetContent(message.MobileNo, message.MessageType);

                db.MessageSet.Add(msg);
                db.SaveChanges();

                SendMessageAsync(msg);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, message);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = msg.MessageId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        private static string GetContent(string mobileNo, MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Register:
                    string validationCode = new Random().Next(100000, 999999).ToString();
                    ValidatingHelper.PutCode(mobileNo, ValidationEntrance.Register, validationCode);
                    return "您注册行李网的登录账号（手机号）所需的验证码为" + validationCode + "，5分钟内有效。";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void SendMessageAsync(Message message)
        {
            string content = message.Content + "【行李网】";
            string uri = string.Format("http://cf.lmobile.cn/submitdata/Service.asmx/g_Submit?sname=dlshbgxx&spwd=Vp1GWFV0&scorpid=&sprdid=1012818&sdst={0}&smsg={1}", message.MobileNo, content);

            Task t = new Task(() =>
            {
                using (var client = new HttpClient())
                {
                    message.ResultCode = client.GetStringAsync(uri).Result;
                }
                using (IbagsDbContext db = new IbagsDbContext())
                {
                    db.Entry(message).State = EntityState.Modified;
                    db.SaveChanges();
                }
            });
            t.Start();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}