using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JMAR.AXAM.WEBAPI.Models;
using JMAR.SYSTEM.DOMAIN.Entities;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace JMAR.AXAM.WEBAPI.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IConfiguration _Configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration Configuration)
        {
            _logger = logger;
            _Configuration = Configuration;
        }

        public IActionResult Index()
        {

            var RetLst = GetProducts();

            return View(RetLst.Result);
        }

        public async Task<List<ProductOutputViewModel>> GetProducts()
        {
            List<ProductOutputViewModel> _Result = new List<ProductOutputViewModel>();

            string UriAddres = _Configuration["URLServicio"];
            var responseBody = String.Empty;
            string Token = HttpContext.Session.GetString("Token");
            try
            {
                using (var Client = new HttpClient())
                {
                    Client.BaseAddress = new Uri(UriAddres);

                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    Task<HttpResponseMessage> response1 = Client.GetAsync(new Uri($"{UriAddres}products?AllowPaging=false"));
                    response1.Result.EnsureSuccessStatusCode();
                    responseBody = await response1.Result.Content.ReadAsStringAsync();
                    _Result = JsonConvert.DeserializeObject<List<ProductOutputViewModel>>(responseBody);
                }
                return _Result;
            }
            catch (Exception Ex)
            {
                return new List<ProductOutputViewModel>();
            }
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
