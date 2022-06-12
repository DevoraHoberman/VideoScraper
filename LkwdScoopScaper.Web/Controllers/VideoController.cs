using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoScraper.Scraping;

namespace LkwdScoopScraper.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        [Route("scrape")]
        [HttpGet]
        public List<Video> Scrape()
        {
            return VideosScraper.Scraper();

        }
    }
}
