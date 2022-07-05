using System.Web.Http;

namespace CourseDiary.Server.Controllers
{
    [RoutePrefix("api/v1/status")]
    public class StatusController : ApiController
    {
        [HttpGet()]
        [Route("")]
        public string Status()
        {
            return "Status OK CourseDiary.Server";
        }
    }
}
