using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Web.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IConfiguration configuration;

        public ConfigurationController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [AcceptVerbs("GET")]
        public ActionResult GetConfigurationValue(string sectionName, string paramName)
        {
            var parameterValue = configuration[$"BingMaps:ApiKey"];
            return Json(new { parameter = parameterValue });
        }
    }
}