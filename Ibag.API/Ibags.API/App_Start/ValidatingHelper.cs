using Ibags.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Ibags.API.App_Start
{
    public class ValidatingHelper
    {
        public static void PutCode(string mobileNo, ValidationEntrance entrance, string code)
        {
            string key = entrance.ToString() + mobileNo;
            HttpRuntime.Cache.Insert(key, code, null, DateTime.Now.AddSeconds(60), Cache.NoSlidingExpiration);
        }

        public static bool Validate(string mobileNo, ValidationEntrance entrance, string code)
        {
            string key = entrance.ToString() + mobileNo;
            bool pass = code == HttpRuntime.Cache[key].ToString();
            if (pass)
            {
                HttpRuntime.Cache.Remove(key);
            }
            return pass;
        }
    }
}