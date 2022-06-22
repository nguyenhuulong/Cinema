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
    public class HumanResourceController : ApiController
    {
        private CinemaDB db = new CinemaDB();

        //GetAdminInfo
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(AdminInfo))]
        public async Task<IHttpActionResult> GetAdminInfo()
        {
            var result = db.Database.SqlQuery<AdminInfo>("exec GetAdminInfo");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Json(result);
        }

        //GetListDepartment
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(DEPARTMENT))]
        public async Task<IHttpActionResult> GetListDepartment()
        {
            var result = await db.DEPARTMENTs.ToListAsync();
           
            if (result == null)
            {
                return NotFound();
            }
            return Json(result);
        }

        //FindMovieById
        [ResponseType(typeof(ADMIN_ACCOUNT))]
        public async Task<IHttpActionResult> FindAdminById(string id)
        {
            ADMIN_ACCOUNT result = await db.ADMIN_ACCOUNT.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        //AddAdmin
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(ADMIN_ACCOUNT))]

        public async Task<IHttpActionResult> AddAdmin(JObject jObject)
        {
            ADMIN_ACCOUNT result = JsonConvert.DeserializeObject<ADMIN_ACCOUNT>(jObject.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.ADMIN_ACCOUNT.Add(result);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        //EditAdmin
        [System.Web.Http.HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> EditAdmin(string id, JObject Movie)
        {

            ADMIN_ACCOUNT result = JsonConvert.DeserializeObject<ADMIN_ACCOUNT>(Movie.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != result.AdminID.ToString())
            {
                return BadRequest();
            }

            db.Entry(result).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        //DeleteAdmin
        [ResponseType(typeof(ADMIN_ACCOUNT))]
        public async Task<IHttpActionResult> DeleteAdmin(string id)
        {
            ADMIN_ACCOUNT result = await db.ADMIN_ACCOUNT.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            db.ADMIN_ACCOUNT.Remove(result);
            await db.SaveChangesAsync();
            return Ok(result);
        }

        //FindDepartmentById
        [ResponseType(typeof(ADMIN_ACCOUNT))]
        public async Task<IHttpActionResult> FindDepartmentById(string id)
        {
            DEPARTMENT result = await db.DEPARTMENTs.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        //AddDepartment
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(ADMIN_ACCOUNT))]

        public async Task<IHttpActionResult> AddDepartment(JObject jObject)
        {
            DEPARTMENT result = JsonConvert.DeserializeObject<DEPARTMENT>(jObject.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.DEPARTMENTs.Add(result);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        //EditDepartment
        [System.Web.Http.HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> EditDepartment(string id, JObject Movie)
        {

            DEPARTMENT result = JsonConvert.DeserializeObject<DEPARTMENT>(Movie.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != result.DepartmentID)
            {
                return BadRequest();
            }

            db.Entry(result).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        //DeleteDepartment
        [ResponseType(typeof(ADMIN_ACCOUNT))]
        public async Task<IHttpActionResult> DeleteDepartment(string id)
        {
            DEPARTMENT result = await db.DEPARTMENTs.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            db.DEPARTMENTs.Remove(result);
            await db.SaveChangesAsync();
            return Ok(result);
        }
    }
}
