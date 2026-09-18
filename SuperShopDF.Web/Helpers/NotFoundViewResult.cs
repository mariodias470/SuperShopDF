using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SuperShopDF.Web.Helpers
{
    public class NotFoundViewResult : ViewResult
    {
        public NotFoundViewResult(string viewName)
        {
            ViewName = viewName;
            StatusCode = (int)HttpStatusCode.NotFound;
        }



    } // end class NotFoundViewResult
} // end namespace SuperShopDF.Web.Helpers
