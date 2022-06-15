using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CinemaAPI.Models;

namespace CinemaAPI.Controllers
{
    public class MoviesController : ApiController
    {
        private CinemaDB db = new CinemaDB();

        // GET: api/Movies
        public IQueryable<MOVIE> GetMOVIEs()
        {
            return db.MOVIEs;
        }

        // GET: api/Movies/5
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> GetMOVIE(string id)
        {
            MOVIE mOVIE = await db.MOVIEs.FindAsync(id);
            if (mOVIE == null)
            {
                return NotFound();
            }

            return Json(mOVIE);
        }
        [HttpGet]
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> MovieCurrent()
        {
            var mOVIE = db.Database.SqlQuery<MOVIE>("exec GetCurrentFilm");
            await mOVIE.ToListAsync();
            if (mOVIE == null)
            {
                return NotFound();
            }

            return Json(mOVIE);
        }
        // PUT: api/Movies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMOVIE(string id, MOVIE mOVIE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mOVIE.MovieID)
            {
                return BadRequest();
            }

            db.Entry(mOVIE).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MOVIEExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Movies
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> PostMOVIE(MOVIE mOVIE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MOVIEs.Add(mOVIE);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MOVIEExists(mOVIE.MovieID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mOVIE.MovieID }, mOVIE);
        }

        // DELETE: api/Movies/5
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> DeleteMOVIE(string id)
        {
            MOVIE mOVIE = await db.MOVIEs.FindAsync(id);
            if (mOVIE == null)
            {
                return NotFound();
            }

            db.MOVIEs.Remove(mOVIE);
            await db.SaveChangesAsync();

            return Ok(mOVIE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MOVIEExists(string id)
        {
            return db.MOVIEs.Count(e => e.MovieID == id) > 0;
        }
    }
}