using System;
using System.Web.Mvc;
using Demos.Web.Models;

namespace Demos.Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Lazy<GithubOAuthApi> githubApi = new Lazy<GithubOAuthApi>(GithubOAuthApi.Create);

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Authorize()
        {
            var githubOAuthApi = githubApi.Value;
            var gitHubLoginUrl = githubOAuthApi.CreateGitHubAuthorizeUrl();
            return Redirect(gitHubLoginUrl);
        }

        public ActionResult AuthorizeCallback(string code)
        {
            var githubOAuthApi = githubApi.Value;
            //check state?
            var getAccessTokenArgs = new GetAccessTokenArgs()
            {
                code = code,
                client_id = githubOAuthApi.ClientId,
                client_secret = githubOAuthApi.ClientSecret
            };
            
            var accessTokenDto = githubOAuthApi.GetAccessToken(getAccessTokenArgs);
            if (accessTokenDto != null)
            {
                var currentUser = githubOAuthApi.GetCurrentUser(accessTokenDto.access_token, accessTokenDto.token_type);
                if (currentUser != null)
                {
                    //"login": "foo",
                    // "id": foo,
                    // "node_id": "foo",
                    // "avatar_url": "foo",
                    // ...
                    var userInfo = new UserInfo() { HeadUrl = currentUser.avatar_url, UserId = currentUser.id, Username = currentUser.login };
                    DemoHelper.CurrentUser = userInfo;
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Reset()
        {
            DemoHelper.CurrentUser = null;
            return RedirectToAction("Index");
        }
    }
}