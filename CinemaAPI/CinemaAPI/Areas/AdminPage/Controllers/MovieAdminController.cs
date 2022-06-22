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
    public class MovieAdminController : ApiController
    {
        private CinemaDB db = new CinemaDB();

        //GetMaxMovieID
        [HttpGet]
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> GetMaxMovieID()
        {
            var result = db.Database.SqlQuery<string>("exec GetMaxMovieID");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Json(result);
        }


        //FindMovieById
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> FindMovieById(string id)
        {
            MOVIE result = await db.MOVIEs.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        //AddMovie
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(MOVIE))]

        public async Task<IHttpActionResult> AddMovie(JObject jObject)
        {
            MOVIE result = JsonConvert.DeserializeObject<MOVIE>(jObject.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.MOVIEs.Add(result);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        //EditMovie
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> EditMovie(string id, JObject Movie)
        {

            MOVIE result = JsonConvert.DeserializeObject<MOVIE>(Movie.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != result.MovieID)
            {
                return BadRequest();
            }

            db.Entry(result).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        //DeleteMovie
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> DeleteMovie(string id)
        {
            MOVIE result = await db.MOVIEs.FindAsync(id);         
            if (result == null)
            {
                return NotFound();
            }
            db.MOVIEs.Remove(result);
            await db.SaveChangesAsync();
            return Ok(result);
        }
    }
}
