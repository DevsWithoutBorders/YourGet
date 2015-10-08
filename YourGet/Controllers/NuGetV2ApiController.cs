using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using YourGet.Core;
using YourGet.Core.Services;

namespace YourGet.Controllers
{
    public class NuGetV2ApiController : Controller
    {
        IContentService ContentService { get; set; }

        protected NuGetV2ApiController() { }

        public NuGetV2ApiController(IContentService contentService)
        {
            ContentService = contentService;
        }

        public async Task<ActionResult> Team()
        {
            var team = await ContentService.GetContentItemAsync(Constants.ContentNames.Team, TimeSpan.FromHours(1));
            return Content(team.ToString(), "application/json");
        }
    }
}