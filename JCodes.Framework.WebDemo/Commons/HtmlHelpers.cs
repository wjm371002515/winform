using System.Web.Mvc;

namespace JCodes.Framework.WebUI.Common
{
    public static class HtmlHelpers
    {
        public static bool HasFunction(this HtmlHelper helper, string functionId)
        {
            return Permission.HasFunction(functionId);
        }

        public static bool IsAdmin()
        {
            return Permission.IsAdmin();
        }
    }
}