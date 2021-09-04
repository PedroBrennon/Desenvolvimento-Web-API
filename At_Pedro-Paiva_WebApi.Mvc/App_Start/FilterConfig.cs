using System.Web;
using System.Web.Mvc;

namespace At_Pedro_Paiva_WebApi.Mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
