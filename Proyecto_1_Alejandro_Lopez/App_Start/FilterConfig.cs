using System.Web;
using System.Web.Mvc;

namespace Proyecto_1_Alejandro_Lopez
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
