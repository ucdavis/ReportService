using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ReportService.Attributes
{
    public class AuthTokenFilter : System.Web.Http.Filters.AuthorizationFilterAttribute
    {
        private static readonly string ValidToken = WebConfigurationManager.AppSettings["token"];

        /// <summary>
        /// Look in the X-Auth-Token header and also the params for a token, and then check it against the proper value
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //First check the preferred token header
            var tokenHeader =
                actionContext.Request.Headers.SingleOrDefault(
                    h => string.Equals(h.Key, "X-Auth-Token", StringComparison.InvariantCultureIgnoreCase));

            if (tokenHeader.Value != null &&
                string.Equals(tokenHeader.Value.First(), ValidToken, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            //Else try the query string token
            var query = HttpUtility.ParseQueryString(actionContext.Request.RequestUri.Query);

            if (string.Equals(query["token"], ValidToken, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            //Nothing is valid, so reject
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}