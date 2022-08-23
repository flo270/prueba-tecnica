using System.Web;
using System.Web.Mvc;

namespace PruebaTecnica_elecciones
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
