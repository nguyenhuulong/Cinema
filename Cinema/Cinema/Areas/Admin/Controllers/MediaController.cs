using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;


namespace Cinema.Areas.Admin.Controllers
{
    public class MediaController : Controller
    {
        private readonly RestClient _client;
        public MediaController()
        {
            _client = new RestClient("http://localhost:8085/Help");
        }
        // GET: Admin/Media
        public ActionResult Media()
        {
            var ID = (Session["Role"]).ToString();
            if (ID == "1" || ID == "3")
            {
                ViewBag.name = Session["AdminID"];

                List<JObject> sobaidang = new List<JObject>(9999);
                List<JObject> soblog = new List<JObject>(9999);
                List<JObject> sobaibl = new List<JObject>(9999);
                List<JObject> sobaitin = new List<JObject>(9999);
                List<JObject> phanhoi = new List<JObject>(9999);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/Admin/"); // ???
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));



                var responseMessage = client.GetAsync("GetPost");
                responseMessage.Wait();
                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        sobaidang.Add(o);
                    }
                    ViewBag.sobaidang = sobaidang.Count;
                }

                responseMessage = client.GetAsync("GetAllBlog");
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        soblog.Add(o);
                    }
                    ViewBag.soblog = soblog.Count;
                }

                responseMessage = client.GetAsync("GetAllReview");
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        sobaibl.Add(o);
                    }
                    ViewBag.sobaibl = sobaibl.Count;
                }

                responseMessage = client.GetAsync("GetAllSale");
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        sobaitin.Add(o);
                    }
                    ViewBag.sobaitin = sobaitin.Count;
                }

                responseMessage = client.GetAsync("GetFeedBack");
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        phanhoi.Add(o);
                    }
                    ViewBag.phanhoi = phanhoi.Count;
                }
                return View();
            }
            else
                return RedirectToAction("Homepage", "Admin");
        }

        public ActionResult AddPostView()
        {
            List<JObject> admins = new List<JObject>(9999);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Media/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));



            var responseMessage = client.GetAsync("GetAdminAccount");
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    admins.Add(o);
                }
                ViewBag.admins = admins;
            }
            return View();
        }
        public ActionResult AddPost()
        {
            ViewBag.admins = db.ADMIN_ACCOUNT.ToList();
            string postName = Request.Form["post-name"];
            string postCategory = Request.Form["post-category"];
            string adminId = Request.Form["admin-id"];
            string max = db.Database.SqlQuery<String>("exec GetMaxPostId").ToList()[0];
            int intMax = Int32.Parse(max.Substring(4, max.Length - 4)) + 1;
            string sign = "POST";
            for (int i = 1; i <= (max.Length - (Int32.Parse(max.Substring(4, max.Length - 4))).ToString().Length) - 4; i++)
            {
                sign += "0";
            }
            string newPostId = sign + intMax.ToString();
            POST newPost = new POST();
            newPost.PostID = newPostId;
            newPost.PostTitle = postName;
            newPost.PostCategory = postCategory;
            newPost.AdminID = Convert.ToInt32(adminId);
            newPost.CreateAt = DateTime.Now.ToLocalTime();
            db.POSTs.Add(newPost);
            db.SaveChanges();
            return RedirectToAction("/Media");
        }
        public ActionResult EditPostView(string postId)
        {
            List<JObject> admins = new List<JObject>(9999);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Media/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));



            var responseMessage = client.GetAsync("GetAdminAccount");
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    admins.Add(o);
                }
                ViewBag.admins = admins;
            }
            List<JObject> post = new List<JObject>(9999);
            responseMessage = client.GetAsync("FindPostById?id=" + postId);
            responseMessage.Wait();
            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    post.Add(o);
                }
                ViewBag.post = post;
            }
            ViewBag.postId = postId;
            return View();
        }

        public ActionResult EditPost(string postId)
        {
            var admins = db.ADMIN_ACCOUNT.ToList();
            ViewBag.admins = admins;
            string postName = Request.Form["post-name"];
            string postCategory = Request.Form["post-category"];
            string adminId = Request.Form["admin-id"];
            POST editedPost = db.POSTs.Find(postId);
            editedPost.PostTitle = postName;
            editedPost.PostCategory = postCategory;
            editedPost.AdminID = Convert.ToInt32(adminId);
            db.SaveChanges();
            return RedirectToAction("/Media");
        }
        public ActionResult DeletePost(string postId)
        {
            var request = new RestRequest($"api/Media/DeletePost/{postId}", Method.Delete);
            _client.Execute(request);
            return RedirectToAction("/Media");
        }
    }
}
