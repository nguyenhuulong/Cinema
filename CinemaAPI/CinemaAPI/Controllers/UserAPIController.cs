using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CinemaAPI.Controllers
{
    public class UserAPIController : ApiController
    {
        private CinemaDB db = new CinemaDB();

        //CheckID
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(User_ID))]
        public async Task<IHttpActionResult> CheckID()
        {
            var result = db.Database.SqlQuery<User_ID>("exec check_ID");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Json(result.ElementAt(0));
        }
        //CheckDataUserAccount
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{email}/{Username}/{UserID}")]
        public async Task<IHttpActionResult> CheckDataUserAccount([FromRoute] string email, [FromRoute] string Username, [FromRoute] string UserID)
        {
            var check = db.USER_ACCOUNT.FirstOrDefaultAsync(s => s.email == email);
            await check;
            var check1 = db.USER_ACCOUNT.FirstOrDefaultAsync(s => s.UserID == UserID);
            await check1;
            var check2 = db.USER_ACCOUNT.FirstOrDefaultAsync(s => s.Username == Username);
            await check2;
            if (check1.Result == null && check2.Result == null && check.Result == null)
            {
                return Json(UserID);
            }
            return NotFound();
        }
        //AddUser
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddUser(JObject jObject)
        {
            USER_ACCOUNT result = JsonConvert.DeserializeObject<USER_ACCOUNT>(jObject.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.USER_ACCOUNT.Add(result);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        //CheckLogin
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{Username}/{Password}")]
        public async Task<IHttpActionResult> CheckLogin([FromRoute] string Username, [FromRoute] string Password)
        {
            var result = db.USER_ACCOUNT.Where(s => s.Username.Equals(Username) && s.UserPassword.Equals(Password));
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Json(result);
        }

        //FindUserByID
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(USER_ACCOUNT))]
        public async Task<IHttpActionResult> FindUserByID(string id)
        {
            var result = await db.USER_ACCOUNT.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Json(result);
        }
        //GetListBillFromUser
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{UserID}")]
        public async Task<IHttpActionResult> GetListBillFromUser([FromRoute] string UserID)
        {
            var result = db.Database.SqlQuery<Bill_Info>($"exec GetListBillFromUser N'{UserID}'");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        //GetTicketsFromTicketSession
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{TicketSession}")]
        public async Task<IHttpActionResult> GetTicketsFromTicketSession([FromRoute] string TicketSession)
        {
            var result = db.Database.SqlQuery<TICKET_2>($"exec GetTicketsFromTicketSession N'{TicketSession}'");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }


        //GetServiceFromServiceSession
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{TicketSession}")]
        public async Task<IHttpActionResult> GetServicesFromServiceSession([FromRoute] string ServiceSession)
        {
            var result = db.Database.SqlQuery<SERVICE_TO_CASH>($"exec GetServicesFromServiceSession N'{ServiceSession}'");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }
        //GetInfoBillFromTicketSession

        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{TicketSession}")]
        public async Task<IHttpActionResult> GetInfoBillFromTicketSession([FromRoute] string TicketSession)
        {
            var result = db.Database.SqlQuery<Bill_data>($"exec GetInfoBillFromTicketSession N'{TicketSession}'");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

    }
}
