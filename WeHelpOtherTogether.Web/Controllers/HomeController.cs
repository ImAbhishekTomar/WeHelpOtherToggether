using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeHelpOtherTogether.Web.Models;

namespace WeHelpOtherTogether.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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


        public IActionResult PostTweet()
        {
            string username = "whot@outlook.in";
            string password = "afxpt@123X";
            string tweet = "Hi, this tweet send by the program";

            try
            {
                string user = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes("status=" + tweet);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://twitter.com/statuses/update.xml");

                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;
                request.Headers.Add("Authorization", "Basic " + user);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bytes.Length;

                Stream reqStream = request.GetRequestStream();
                reqStream.Write(bytes, 0, bytes.Length);
                reqStream.Close();

                return View("index");
            }
            catch (Exception ex)
            {
                return View("index");
            }
        }
    }
}
