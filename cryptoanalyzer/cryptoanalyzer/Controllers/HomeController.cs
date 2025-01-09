using cryptoanalyzer.Models;
using Microsoft.AspNetCore.Mvc;

namespace cryptoanalyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICryptoRepository cryptoRepository;

        public HomeController(ICryptoRepository cryptoRepository)
        {
            this.cryptoRepository = cryptoRepository;
        }
        public IActionResult Index()
        {
            ViewBag.future = cryptoRepository.GetAllFuturePrices();
            return View(cryptoRepository.GetAllCucrypto());
        }
    }
}
 