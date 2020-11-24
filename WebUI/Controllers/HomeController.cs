using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Clients;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IImageClient _imageClient;
        private readonly IPriceClient _priceClient;
        private readonly IProductClient _productClient;

        public HomeController(ILogger<HomeController> logger, IImageClient imageClient, IPriceClient priceClient,IProductClient productClient)
        {
            _logger = logger;
            _imageClient = imageClient;
            _priceClient = priceClient;
            _productClient = productClient;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productClient.GetAll());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
