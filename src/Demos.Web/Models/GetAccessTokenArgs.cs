namespace Demos.Web.Models
{
    public class GetAccessTokenArgs
    {
        //Required
        public string client_id { get; set; }
        //Required
        public string client_secret { get; set; }
        //Required
        public string code { get; set; }

        public string redirect_uri { get; set; }
        public string state { get; set; }
    }
}