using ContactManager.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Web.Controllers
{
    public class BingAddressController : Controller
    {

        private readonly BingMapsService _bingMapsService;

        public BingAddressController(BingMapsService bingMapsService)
        {
            _bingMapsService = bingMapsService;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> ValidateAddress(string FormattedAddress)
        {

            var validation = await _bingMapsService.ValidateAddress(FormattedAddress);

            if (validation.IsValid)
            {
                return Json(true);
            }
            else
            {
                return Json($"Address {FormattedAddress} is not correct.");
            }
        }
    }
}
