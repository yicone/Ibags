using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ibags.API.Models
{
    public class MessageModel
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string MobileNo { get; set; }
        /// <summary>
        /// 短信类型
        /// </summary>
        public MessageType MessageType { get; set; }
    }
}