namespace Demos.Web.Models
{
    public class GithubConfig
    {
        public string ClientId { get; set; }
        public string ClientSecrect { get; set; }

        private static GithubConfig config = null;
        public static GithubConfig GetConfig()
        {
            //todo read from config
            if (config == null)
            {
                config = new GithubConfig();
                config.ClientId = "e7b456c2d1557a806832";
                config.ClientSecrect = "f9bc403197f9a62002ef509e6b72e2e6dda2691d";
            }
            return config;
        }
    }
}