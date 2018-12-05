using System;
using System.Net;
using System.Net.Http;

namespace Demos.Web.Models
{
    public class GithubOAuthApi
    {
        public string CreateGitHubAuthorizeUrl()
        {
            return string.Format("https://github.com/login/oauth/authorize?client_id={0}" , WebUtility.UrlEncode(ClientId));
        }

        public AccessTokenDto GetAccessToken(GetAccessTokenArgs args)
        {
            //doc => https://developer.github.com/apps/building-oauth-apps/authorizing-oauth-apps/

            var httpClient = WebApiHelper.Create(true);
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("OAuthDemoClient");
            var response = httpClient.PostAsJsonAsync(OAuth2TokenEndpoint, args).Result;
            if (response.IsSuccessStatusCode)
            {
                var token = response.Content.ReadAsStringAsync().Result;
                var uri = new Uri("http://localhost?" + token);
                var nameValueCollection = uri.ParseQueryString();

                var accessTokenDto = new AccessTokenDto();
                accessTokenDto.access_token = nameValueCollection["access_token"];
                accessTokenDto.token_type = nameValueCollection["token_type"];
                return accessTokenDto;
            }
            return null;
        }
        
        public dynamic GetCurrentUser(string token, string tokenType)
        {
            var getUrl = string.Format("https://api.github.com/user?access_token={0}&token_type={1}", WebUtility.UrlEncode(token), WebUtility.UrlEncode(tokenType));
            var result = WebApiHelper.GetAsJson<dynamic>(getUrl);
            return result;
        }

        public GithubOAuthApi(string clientId, string clientSecret)
        {
            OAuth2AuthorizeEndpoint = "https://github.com/login/oauth/authorize";
            OAuth2TokenEndpoint = "https://github.com/login/oauth/access_token";
            ClientId = clientId;
            ClientSecret = clientSecret;
            State = "abc";  //todo random
            Scopes = "user";
        }
        public string OAuth2AuthorizeEndpoint { get; set; }
        public string OAuth2TokenEndpoint { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string State { get; set; }
        public string Scopes { get; set; }
        
        public static GithubOAuthApi Create()
        {
            var githubConfig = GithubConfig.GetConfig();
            var githubOAuthApi = new GithubOAuthApi(githubConfig.ClientId, githubConfig.ClientSecrect);
            return githubOAuthApi;
        }
    }
}