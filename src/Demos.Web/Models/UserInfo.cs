using System;

namespace Demos.Web.Models
{
    public class UserInfo
    {
        public UserInfo()
        {
            CreateDate = DateTime.Now;
        }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string HeadUrl { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
