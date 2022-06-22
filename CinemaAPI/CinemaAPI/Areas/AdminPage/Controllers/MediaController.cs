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

namespace CinemaAPI.Areas.AdminPage.Controllers
{
    public class MediaController : ApiController
    {
        private CinemaDB db = new CinemaDB();

        //GetMaxPostID
        [HttpGet]
        [ResponseType(typeof(POST))]
        public async Task<IHttpActionResult> GetMaxPostID()
        {
            var result = db.Database.SqlQuery<string>("exec GetMaxPostID");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Json(result);
        }


        //FindMovieById
        [ResponseType(typeof(POST))]
        public async Task<IHttpActionResult> FindPostById(string id)
        {
            POST result = await db.POSTs.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        //AddPost
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(POST))]

        public async Task<IHttpActionResult> AddPost(JObject jObject)
        {
            POST result = JsonConvert.DeserializeObject<POST>(jObject.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.POSTs.Add(result);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        //EditMovie
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> EditPost(string id, JObject Movie)
        {

            POST result = JsonConvert.DeserializeObject<POST>(Movie.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != result.PostID)
            {
                return BadRequest();
            }

            db.Entry(result).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        //DeletePost
        [ResponseType(typeof(POST))]
        public async Task<IHttpActionResult> DeletePost(string id)
        {
            POST result = await db.POSTs.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            db.POSTs.Remove(result);
            await db.SaveChangesAsync();
            return Ok(result);
        }
    }
}
