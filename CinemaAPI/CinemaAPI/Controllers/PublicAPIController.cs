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
    public class PublicAPIController : ApiController
    {
        private CinemaDB db = new CinemaDB();

        // GET: api/PublicAPI
        [HttpGet]
        [ResponseType(typeof(CINEMA_LOCATION))]
        public async Task<IHttpActionResult> GetLocationInfo()
        {
            var locations = db.Database.SqlQuery<CINEMA_LOCATION>("exec GetLocationInfo");
            await locations.ToListAsync();
            if (locations == null)
            {
                return NotFound();
            }

            return Json(locations);
        }
/*
        // GET: api/PublicAPI/5
        [ResponseType(typeof(CINEMA))]
        public async Task<IHttpActionResult> GetCINEMA(string id)
        {
            CINEMA cINEMA = await db.CINEMAs.FindAsync(id);
            if (cINEMA == null)
            {
                return NotFound();
            }

            return Ok(cINEMA);
        }

        // PUT: api/PublicAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCINEMA(string id, CINEMA cINEMA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cINEMA.CinemaID)
            {
                return BadRequest();
            }

            db.Entry(cINEMA).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CINEMAExists(id))
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

        // POST: api/PublicAPI
        [ResponseType(typeof(CINEMA))]
        public async Task<IHttpActionResult> PostCINEMA(CINEMA cINEMA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CINEMAs.Add(cINEMA);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CINEMAExists(cINEMA.CinemaID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cINEMA.CinemaID }, cINEMA);
        }

        // DELETE: api/PublicAPI/5
        [ResponseType(typeof(CINEMA))]
        public async Task<IHttpActionResult> DeleteCINEMA(string id)
        {
            CINEMA cINEMA = await db.CINEMAs.FindAsync(id);
            if (cINEMA == null)
            {
                return NotFound();
            }

            db.CINEMAs.Remove(cINEMA);
            await db.SaveChangesAsync();

            return Ok(cINEMA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CINEMAExists(string id)
        {
            return db.CINEMAs.Count(e => e.CinemaID == id) > 0;
        }*/
    }
}