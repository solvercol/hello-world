using System.Text.RegularExpressions;
using System.Web;

namespace Infrastructure.CrossCutting.NetFramework.Util
{
    public class UrlUtil
    {
        private UrlUtil()
        {
        }

        public static bool ValidarUrl(string url)
        {

            var cmpUrl = System.Globalization.CultureInfo.InvariantCulture.CompareInfo;

            const string strRegex = @"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";
            if (cmpUrl.IsPrefix(url, "http://") == false)
            {
                url = "http://" + url;
            }

            return Regex.IsMatch(url, strRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);
        }

        /// <summary>
        /// GetApplicationPath returns the base application path and ensures that it allways ends with a "/".
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationPath()
        {
            return Text.EnsureTrailingSlash(HttpContext.Current.Request.ApplicationPath);
        }

        /// <summary>
        /// Get the (lowercase) url of the site without any trailing slashes.
        /// </summary>
        /// <returns></returns>
        public static string GetSiteUrl()
        {
            var path = HttpContext.Current.Request.ApplicationPath;
            if (path != null)
            {
                if (path.EndsWith("/") && path.Length == 1)
                {
                    return GetHostUrl();
                }
                else
                {
                    return GetHostUrl() + path.ToLower();
                }
            }
            return "";
        }


        /// <summary>
        /// Returns a formatted url for a given section (/{ApplicationPath}/{Section.Id}/section.aspx).
        /// </summary>
        /// <param name="idSection"></param>
        /// <returns></returns>
        public static string GetUrlFromSection(string idSection)
        {
            return GetApplicationPath() + idSection + "/section.aspx";
        }


        /// <summary>
        /// Get the full url of a Section with the host url resolved via the Site property.
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public static string GetFullUrlFromSectionViaSite(string siteUrl, string sectionId)
        {
            if (siteUrl != "" && sectionId != "")
            {
                return Text.EnsureTrailingSlash(siteUrl) + sectionId + "/section.aspx";
            }
            return null;
        }


        /// <summary>
        /// Get the full url of a Node with the host url resolved via the Site property.
        /// </summary>
        /// <param name="isExternalLink"></param>
        /// <param name="siteUrl"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static string GetFullUrlFromNodeViaSite(bool isExternalLink, string siteUrl, string nodeId)
        {
            if (!isExternalLink)
            {
                return Text.EnsureTrailingSlash(siteUrl) + nodeId + "/view.aspx";
            }
            return null;
        }

        /// <summary>
        /// Returns a formatted url for a given section (http://{hostname}/{ApplicationPath}/{Section.Id}/section.aspx).
        /// </summary>
        /// <param name="idSection"></param>
        /// <returns></returns>
        public static string GetFullUrlFromSection(string idSection)
        {
            return GetHostUrl() + GetUrlFromSection(idSection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathInfo"></param>
        /// <returns></returns>
        public static string[] GetParamsFromPathInfo(string pathInfo)
        {
            if (pathInfo.Length > 0)
            {
                if (pathInfo.EndsWith("/"))
                {
                    pathInfo = pathInfo.Substring(0, pathInfo.Length - 1);
                }
                pathInfo = pathInfo.Substring(1, pathInfo.Length - 1);
                return pathInfo.Split(new char[] { '/' });
            }
            else
            {
                return null;
            }
        }


        private static string GetHostUrl()
        {
            var securePort = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            var protocol = securePort == null || securePort == "0" ? "http" : "https";
            var serverPort = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            var port = serverPort == "80" ? string.Empty : ":" + serverPort;
            var serverName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            return string.Format("{0}://{1}{2}", protocol, serverName, port);
        }

        public static string GetUrlFromNode(string idNode)
        {
            return GetApplicationPath() + idNode + "/view.aspx";
        }

        public static string GetUrlPreViewDocumentforEmail()
        {
            var appRot = GetSiteUrl();
            var idModule = HttpContext.Current.Request.QueryString["ModuleId"];
            var idPedido = HttpContext.Current.Request.QueryString["IdPedido"];

            var url = string.Format("{0}/Pages/Modules/Pedidos/Catalogos/FrmPreViewPedido.aspx?ModuleId={1}&IdPedido={2}", appRot, idModule, idPedido);
            return "<a href=\"" + url + "\"> De click aqu� para abrir el documento.\"</a>";
        }

        public static string GetUrlPreViewForNotificationWindow()
        {
            var idModule = HttpContext.Current.Request.QueryString["ModuleId"];
            var idPedido = HttpContext.Current.Request.QueryString["IdPedido"];
           return  string.Format("~/Pages/Modules/Pedidos/Catalogos/FrmPreViewPedido.aspx?ModuleId={0}&IdPedido={1}", idModule, idPedido);
        }

    }
}