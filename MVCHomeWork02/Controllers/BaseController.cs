using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCHomeWork02.Filter;

namespace MVCHomeWork02.Controllers
{
    [IdFilter]
    public class BaseController : Controller
    {
        protected override void HandleUnknownAction(string actionName)
        {
            if (Request.IsAjaxRequest())
            {
                base.HandleUnknownAction(actionName);
            }
            else
            {
                this.RedirectToAction("Error404", "Home").ExecuteResult(this.ControllerContext);
            }
        }
    }
}