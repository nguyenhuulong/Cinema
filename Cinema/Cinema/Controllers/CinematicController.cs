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
    public class CinematicController : Controller
    {
        // GET: Cinematic
        public ActionResult DetailPost(string id)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            /*Call CurMovie API*/
            List<JObject> listCurMovie = new List<JObject>(9999);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Cinematic/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("CurMovie");
            responseMessage.Wait();

            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listCurMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listCurMovieJA.Children<JObject>())
                {
                    listCurMovie.Add(o);
                }
                ViewBag.curMovie = listCurMovie;
            }
            /*Call GetPostContentFromPostID API*/
            List<JObject> listPostContent = new List<JObject>(9999);
            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri("http://localhost:8085/api/Cinematic/");
            client1.DefaultRequestHeaders.Accept.Clear();
            client1.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            responseMessage = client1.GetAsync("GetPostContentFromPostID?id=" + id);
            responseMessage.Wait();

            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listPostContentJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listPostContentJA.Children<JObject>())
                {
                    listPostContent.Add(o);
                }
                ViewBag.postcontent = listPostContent[0];
            }
            /*Call GetCategoryFromPost API*/
            List<JObject> listPostCategory = new List<JObject>(9999);
            string category;
            HttpClient client2 = new HttpClient();
            client2.BaseAddress = new Uri("http://localhost:8085/api/Cinematic/");
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            responseMessage = client2.GetAsync("GetCategoryFromPost?id=" + id);
            responseMessage.Wait();

            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listPostCategoryJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listPostCategoryJA.Children<JObject>())
                {
                    listPostCategory.Add(o);
                }
                ViewBag.category = listPostCategory[0];
            }
            /*Call GetPostInfoByID API*/
            HttpClient client3 = new HttpClient();
            client3.BaseAddress = new Uri("http://localhost:8085/api/Cinematic/");
            client3.DefaultRequestHeaders.Accept.Clear();
            client3.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            responseMessage = client3.GetAsync("GetPostInfoByID?id=" + id);
            responseMessage.Wait();

            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                ViewBag.post = JObject.Parse(readTask.Result);
            }
            return View();
        }
    }
}