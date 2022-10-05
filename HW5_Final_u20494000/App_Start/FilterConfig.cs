using System.Web;
using System.Web.Mvc;

namespace HW5_Final_u20494000
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
