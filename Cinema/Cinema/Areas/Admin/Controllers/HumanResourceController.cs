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
    public class HumanResourceController : Controller
    {
        private readonly RestClient _client;
        public HumanResourceController()
        {
            _client = new RestClient("http://localhost:8085/Help");
        }
        // GET: Admin/HumanResource
        public ActionResult HumanResource()
        {
            var ID = (Session["Role"]).ToString();
            //var x = db.Database.SqlQuery<String>($"exec getRole '{Session["Role"]}'").FirstOrDefault().ToString();
            if (ID == "1")
            {
                ViewBag.name = Session["AdminID"];
                List<JObject> admins = new List<JObject>(9999);
                List<JObject> departments = new List<JObject>(9999);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/HumanResource/"); // ???
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetAdminInfo"); // 
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
                //////////////////////
                responseMessage = client.GetAsync("GetListDepartment"); // 
                responseMessage.Wait();
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        departments.Add(o);
                    }
                    ViewBag.departments = departments;
                }

                return View();
            }
            else
                return RedirectToAction("Homepage", "Admin");
        }
        public ActionResult AddAdminView()
        {
            List<JObject> admins = new List<JObject>(9999);
            List<JObject> departments = new List<JObject>(9999);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/HumanResource/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetAdminInfo"); // 
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
            //////////////////////
            responseMessage = client.GetAsync("GetListDepartment"); // 
            responseMessage.Wait();
            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    departments.Add(o);
                }
                ViewBag.departments = departments;
            }
            return View();
        }
        public ActionResult AddAdmin()
        {
            var departments = db.DEPARTMENTs.ToList();
            ViewBag.departments = departments;
            string adminName = Request.Form["admin-name"];
            string adminPassword = Request.Form["admin-password"];
            int adminRole = Convert.ToInt32(Request.Form["role"]);
            var admins = db.Database.SqlQuery<AdminInfo>("exec GetAdminInfo").ToList();
            var max = admins[0].AdminID;
            foreach (var admin in admins)
            {
                if (max < admin.AdminID) max = admin.AdminID;
                if (adminName == admin.AdminName)
                {
                    ViewBag.error = "Tên đăng nhập đã tồn tại";
                    return View("~/Areas/Admin/Views/HumanResource/AddAdminView.cshtml");
                }
            }
            int adminId = max + 1;
            ADMIN_ACCOUNT newAdmin = new ADMIN_ACCOUNT();
            newAdmin.AdminID = adminId;
            newAdmin.AdminName = adminName;
            newAdmin.AdminPassword = adminPassword;
            newAdmin.DepartmentID = adminRole.ToString();
            db.ADMIN_ACCOUNT.Add(newAdmin);
            db.SaveChanges();
            return RedirectToAction("/HumanResource");
        }
        public ActionResult EditAdminView(int adminId)
        {
            List<JObject> admin = new List<JObject>(9999);
            List<JObject> departments = new List<JObject>(9999);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/HumanResource/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("FindAdminById?id=" + adminId); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    admin.Add(o);
                }
                ViewBag.admin = admin;
            }
            //////////////////////
            responseMessage = client.GetAsync("GetListDepartment"); // 
            responseMessage.Wait();
            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    departments.Add(o);
                }
                ViewBag.departments = departments;
            }
            ViewBag.adminId = adminId;
            return View();
        }

        public ActionResult EditAdmin(int adminId)
        {
            int adminRole = Convert.ToInt32(Request.Form["role"]);
            ADMIN_ACCOUNT editedAdmin = db.ADMIN_ACCOUNT.Find(adminId);
            editedAdmin.DepartmentID = adminRole.ToString();
            db.SaveChanges();
            return RedirectToAction("/HumanResource");
        }
        public ActionResult DeleteAdmin(int adminId)
        {
            var request = new RestRequest($"api/HumanResource/DeleteAdmin/{adminId}", Method.Delete);
            _client.Execute(request);
            return RedirectToAction("/HumanResource");
        }
        public ActionResult AddDepartmentView()
        {
            return View();
        }
        public ActionResult AddDepartment()
        {
            List<JObject> departments = new List<JObject>(9999);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/HumanResource/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            //////////////////////
            var responseMessage = client.GetAsync("GetListDepartment"); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    departments.Add(o);
                }
                ViewBag.departments = departments;
            }
            string departmentName = Request.Form["department-name"];
            var max = Convert.ToInt32(departments[0].DepartmentID);
            foreach (var department in departments)
            {
                var departmentId = Convert.ToInt32(department.DepartmentID);
                if (max < departmentId) max = departmentId;
                if (departmentName == department.DepartmentName)
                {
                    ViewBag.error = "Tên bộ phận đã tồn tại";
                    return View("~/Areas/Admin/Views/HumanResource/AddDepartmentView.cshtml");
                }
            }
            int newDepartmentId = max + 1;
            DEPARTMENT newDepartment = new DEPARTMENT();
            newDepartment.DepartmentID = newDepartmentId.ToString();
            newDepartment.DepartmentName = departmentName;
            db.DEPARTMENTs.Add(newDepartment);
            db.SaveChanges();
            return RedirectToAction("/HumanResource");
        }
        public ActionResult EditDepartmentView(string departmentId)
        {
            List<JObject> departments = new List<JObject>(9999);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/HumanResource/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            //////////////////////
            var responseMessage = client.GetAsync("FindDepartmentById?id=" + departmentId); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    departments.Add(o);
                }
                ViewBag.departments = departments[0];
            }
            ViewBag.departmentId = departmentId;
            return View();
        }

        public ActionResult EditDepartment(string departmentId)
        {
            string departmentName = Request.Form["department-name"];
            DEPARTMENT editedDepartment = db.DEPARTMENTs.Find(departmentId);
            editedDepartment.DepartmentName = departmentName;
            db.SaveChanges();
            return RedirectToAction("/HumanResource");
        }
        public ActionResult DeleteDepartment(string departmentId)
        {
            var request = new RestRequest($"api/HumanResource/DeleteDepartment/{departmentId}", Method.Delete);
            _client.Execute(request);
            return RedirectToAction("/HumanResource");
        }
    }
}