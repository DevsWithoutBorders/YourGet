using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using YourGet.Core;
using YourGet.Core.Services;

namespace YourGet.Controllers
{
    public class NuGetApiV2Controller : AppController
    {
        public IContentService ContentService
        { get; set; }

        protected NuGetApiV2Controller()
        {
        }

        public NuGetApiV2Controller(IContentService contentService)
        {
            ContentService = contentService;
        }

        public virtual async Task<ActionResult> Team()
        {
            var team = await ContentService.GetContentItemAsync(Constants.ContentNames.Team, TimeSpan.FromHours(1));
            return Content(team.ToString(), "application/json");
        }
    }
}
