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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace CinemaAPI.Areas.AdminPage.Controllers
{
    public class FacilityController : ApiController
    {
        private CinemaDB db = new CinemaDB();
        public IQueryable<CINEMA_LOCATION> GetLocation()
        {
            return db.CINEMA_LOCATION;
        }

        [HttpGet]
        [ResponseType(typeof(CinemaInfo))]
        public async Task<IHttpActionResult> GetCinemaInfo()
        {
            var cinemas = db.Database.SqlQuery<CinemaInfo>("exec GetCinemaInfo");
            await cinemas.ToListAsync();
            if (cinemas == null)
            {
                return NotFound();
            }
            return Json(cinemas);
        }

        [HttpGet]
        [ResponseType(typeof(RoomInfo))]
        public async Task<IHttpActionResult> GetRoomInfo()
        {
            var rooms = db.Database.SqlQuery<RoomInfo>("exec GetRoomInfo");
            await rooms.ToListAsync();
            if (rooms == null)
            {
                return NotFound();
            }
            return Json(rooms);
        }

        public IQueryable<CINEMA> GetCinema()
        {
            return db.CINEMAs;
        }

        [HttpGet]
        [ResponseType(typeof(String))]
        public async Task<IHttpActionResult> GetMaxCinemaID()
        {
            var max = db.Database.SqlQuery<String>("exec GetMaxCinemaId");
            await max.ToListAsync();
            if (max == null)
            {
                return NotFound();
            }
            return Json(max);
        }

        [HttpPost]
        [ResponseType(typeof(CINEMA))]
        public async Task<IHttpActionResult> AddDiscount(CINEMA cINEMA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CINEMAs.Add(cINEMA);

            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cINEMA.CinemaID }, cINEMA);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCinema(string id, CINEMA cINEMA)
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
                if (!Cinemaxists(id))
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

        [HttpDelete]
        [ResponseType(typeof(CINEMA))]
        public async Task<IHttpActionResult> DeleteCinema(string id)
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


        private bool Cinemaxists(string id)
        {
            return db.CINEMAs.Count(e => e.CinemaID == id) > 0;
        }
    }
}
