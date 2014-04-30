using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Ibags.API.App_Start
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Logger.Instance().Error(actionExecutedContext.Exception);
            base.OnException(actionExecutedContext);
        }
    }
}