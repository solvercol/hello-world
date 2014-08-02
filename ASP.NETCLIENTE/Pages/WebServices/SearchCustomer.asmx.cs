using System;
using System.Collections.Generic;
using System.Web.Services;
using Infrastructure.CrossCutting.IoC;

namespace ASP.NETCLIENTE.Pages.WebServices
{
    /// <summary>
    /// Summary description for SearchCustomer
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SearchCustomer : System.Web.Services.WebService
    {
        //private readonly CustomerPresenter _customer;

        public SearchCustomer()
        {
            //_customer = IoC.Resolve<CustomerPresenter>();
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] CustomerByName(string prefixText, int count)
        {
            try
            {
                //var list = _customer.CustomerByName(prefixText);

                //return list.Select(tblMaestraCustomersCustomer => tblMaestraCustomersCustomer.CompanyName).ToArray();
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] CustomerList(string prefixText, int count)
        {
            try
            {
                //var strCompanyList = new List<string>();

                //var list = _customer.CustomerByName(prefixText).ToList();
                //if (list.Count() > 0)
                //{
                //    for (var i = 0; i < list.Count(); i++)
                //    {
                //        strCompanyList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(list[i].CompanyName, Convert.ToString(
                //          list[i].IdCustomer.ToString())));
                //    }
                //}
                //return strCompanyList.ToArray();
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }
       
    }
}
