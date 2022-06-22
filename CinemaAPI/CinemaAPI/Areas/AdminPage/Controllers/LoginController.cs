using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CinemaAPI.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Http.ModelBinding;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaAPI.Areas.AdminPage.Controllers
{
    public class LoginController : ApiController
    {
        // GET: AdminPage/Login
        private CinemaDB db = new CinemaDB();

        //CheckLogin
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{Username}/{Password}")]
        public async Task<IHttpActionResult> CheckLogin([FromRoute] string Username, [FromRoute] string Password)
        {
            var result = db.ADMIN_ACCOUNT.Where(s => s.AdminName.Equals(Username) && s.AdminPassword.Equals(Password));
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Json(result);
        }
    }
}