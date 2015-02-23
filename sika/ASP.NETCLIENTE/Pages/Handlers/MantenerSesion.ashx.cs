using System.Web;

namespace ASP.NETCLIENTE.Pages.Handlers
{
    /// <summary>
    /// Summary description for MantenerSesion
    /// </summary>
    public class MantenerSesion : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetNoStore();
            context.Response.ContentType = "application/x-javascript";
            context.Response.Write("//");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}