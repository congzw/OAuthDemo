using System.Web;

namespace Demos.Web.Models
{
    public class DemoHelper
    {
        private const string _CurrentUser = "_CurrentUser";
        public static UserInfo CurrentUser
        {
            get { return HttpContext.Current.Session[_CurrentUser] as UserInfo; }
            set { HttpContext.Current.Session[_CurrentUser] = value; }
        }
    }
}
