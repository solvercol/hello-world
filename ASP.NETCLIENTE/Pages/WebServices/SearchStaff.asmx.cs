using System;
using System.Collections.Generic;
using System.Web.Services;
using Infrastructure.CrossCutting.IoC;

namespace ASP.NETCLIENTE.Pages.WebServices
{
    /// <summary>
    /// Summary description for SearchStaff
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SearchStaff : System.Web.Services.WebService
    {
        //private readonly StaffPresenter _staff;

        public SearchStaff()
        {
            //_staff = IoC.Resolve<StaffPresenter>();
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] UserByName(string prefixText, int count)
        {
            try
            {
                var strCompanyList = new List<string>();

                var list = _staff.FindByName(prefixText);

                if (list.Count() > 0)
                {
                    for (var i = 0; i < list.Count(); i++)
                    {
                        strCompanyList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(list[i].Nombres, Convert.ToString(
                          list[i].IdStaff.ToString())));
                    }
                }

                return strCompanyList.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
