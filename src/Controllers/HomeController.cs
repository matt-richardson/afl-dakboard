using Microsoft.AspNetCore.Mvc;

namespace afl_dakboard.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => RedirectPermanent("/afl/ladder");

        //handle legacy path
        public IActionResult Ladder() => RedirectPermanent("/afl/ladder");

        //handle legacy path
        public IActionResult Richmond() => RedirectPermanent("/afl/richmond");
    }
}
