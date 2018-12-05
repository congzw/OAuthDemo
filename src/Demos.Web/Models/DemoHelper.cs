using System.Web;

namespace Demos.Web.Models
{
    public class DemoHelper
    {
        private const string _CurrentUser = "_CurrentUser";
        private const string _CurrentResult = "_CurrentResult";
        public static UserInfo CurrentUser
        {
            get { return HttpContext.Current.Session[_CurrentUser] as UserInfo; }
            set { HttpContext.Current.Session[_CurrentUser] = value; }
        }


        public static AuthorizeResult CurrentResult
        {
            get { return HttpContext.Current.Session[_CurrentResult] as AuthorizeResult; }
            set { HttpContext.Current.Session[_CurrentResult] = value; }
        }
    }
}
