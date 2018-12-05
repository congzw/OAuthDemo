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
                    //"login": "congzw",
                    // "id": 8489587,
                    // "node_id": "MDQ6VXNlcjg0ODk1ODc=",
                    // "avatar_url": "https://avatars1.githubusercontent.com/u/8489587?v=4",
                    // ...
                    var userInfo = new UserInfo() { HeadUrl = currentUser.avatar_url, UserId = currentUser.id, Username = currentUser.login };
                    DemoHelper.CurrentUser = userInfo;
                    DemoHelper.CurrentResult = new AuthorizeResult() { Success = true, Message = "Success Authorized At: " + userInfo.CreateDate};
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Clear()
        {
            DemoHelper.CurrentUser = null;
            DemoHelper.CurrentResult = null;
            return RedirectToAction("Index");
        }
    }
}