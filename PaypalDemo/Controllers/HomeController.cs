using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PaypalDemo.Models;
using PaypalDemo.PaypalHelpers;

namespace PaypalDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<PaypalApiSetting> paypalSettings;
        private readonly IConfiguration configuration;

        public HomeController(IOptions<PaypalApiSetting> paypal, IConfiguration configuration)
        {
            this.paypalSettings = paypal;
            this.configuration = configuration;
            var clientId = paypalSettings.Value.ClientID;

        }

        public IActionResult Index()
        {
            ProductModel productModel = new ProductModel();
            ViewBag.Products = productModel.GetAll();
            ViewBag.Total = productModel.GetAll().Sum(p=>p.Price * p.Quantity);

            return View();
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
        [HttpPost]
        public async Task<IActionResult> Checkout(double total)
        {
            var paypalAPI = new PaypalAPI(configuration);
            String URL = await paypalAPI.GetRedirectUrlToPaypal(total, "USD");
            return Redirect(URL);
        }
        /// <summary>
        /// Método para ejecutar el pago
        /// </summary>
        /// <param name="paymentId">ID del pago</param>
        /// <param name="PayerID">ID del pagador</param>
        /// <returns></returns>
        public async Task<IActionResult> success([FromQuery(Name = "paymentId")] string paymentId, [FromQuery(Name = "PayerID")] string PayerID)
        {
            var paypalAPI = new PaypalAPI(configuration);
            var result = await paypalAPI.ExecutePayment(paymentId, PayerID);
            ViewBag.data = result;
            return View("success");
        }

        public IActionResult cancel([FromQuery(Name = "token")] string token)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
