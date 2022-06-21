using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Cinema.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movies
       
        public ActionResult Details(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Movies/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
         
            var responseMessage = client.GetAsync("GetMOVIE/MV0002");
            responseMessage.Wait();

            var result = responseMessage.Result;
            if(result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();           
                ViewBag.detail = JObject.Parse(readTask.Result);
            }
            return View();
        }
    }
}