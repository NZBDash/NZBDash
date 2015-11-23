using System.Web;
using System.Web.Mvc;

namespace NZBDash.UI.Helpers
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlString LoadingSpinner(this HtmlHelper html)
        {
            return html.Raw("<i class='fa fa-refresh fa-spin fa-5x'></i>");
        }
    }
}