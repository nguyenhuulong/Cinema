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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
        public async Task<IHttpActionResult> AddCinema(JObject jObject)
        {
            CINEMA result = JsonConvert.DeserializeObject<CINEMA>(jObject.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CINEMAs.Add(result);

            try
            {
                await db.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {

                if (CinemaExists(result.CinemaID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }

            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [ResponseType(typeof(CINEMA))]
        public async Task<IHttpActionResult> FindCinema(string id)
        {
            var cinema = db.CINEMAs.FindAsync(id);
            await cinema;
            if (cinema == null)
            {
                return NotFound();
            }
            return Json(cinema);
        }


        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> EditCinema(string id, JObject jObject)
        {
            CINEMA cINEMA = JsonConvert.DeserializeObject<CINEMA>(jObject.ToString());
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
                if (!CinemaExists(id))
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

        [HttpGet]
        [ResponseType(typeof(String))]
        public async Task<IHttpActionResult> GetMaxLocationID()
        {
            var max = db.Database.SqlQuery<String>("exec GetMaxLocationId");
            await max.ToListAsync();
            if (max == null)
            {
                return NotFound();
            }
            return Json(max);
        }

        [HttpPost]
        [ResponseType(typeof(CINEMA_LOCATION))]
        public async Task<IHttpActionResult> AddLocation(JObject jObject)
        {
            CINEMA_LOCATION result = JsonConvert.DeserializeObject<CINEMA_LOCATION>(jObject.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CINEMA_LOCATION.Add(result);

            try
            {
                await db.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {

                if (LocationExists(result.LocationID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }

            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [ResponseType(typeof(CINEMA_LOCATION))]
        public async Task<IHttpActionResult> FindLocation(string id)
        {
            var location = db.CINEMA_LOCATION.FindAsync(id);
            await location;
            if (location == null)
            {
                return NotFound();
            }
            return Json(location);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> EditLocation(string id, JObject jObject)
        {
            CINEMA_LOCATION lOCATION = JsonConvert.DeserializeObject<CINEMA_LOCATION>(jObject.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lOCATION.LocationID)
            {
                return BadRequest();
            }

            db.Entry(lOCATION).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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
        [ResponseType(typeof(CINEMA_LOCATION))]
        public async Task<IHttpActionResult> DeleteLocation(string id)
        {
            CINEMA_LOCATION lOCATION = await db.CINEMA_LOCATION.FindAsync(id);
            if (lOCATION == null)
            {
                return NotFound();
            }

            db.CINEMA_LOCATION.Remove(lOCATION);
            await db.SaveChangesAsync();

            return Ok(lOCATION);
        }

        [HttpGet]
        [ResponseType(typeof(String))]
        public async Task<IHttpActionResult> GetMaxRoomID()
        {
            var max = db.Database.SqlQuery<String>("exec GetMaxRoomId");
            await max.ToListAsync();
            if (max == null)
            {
                return NotFound();
            }
            return Json(max);
        }

        [HttpPost]
        [ResponseType(typeof(ROOM))]
        public async Task<IHttpActionResult> AddRoom(JObject jObject)
        {
            ROOM result = JsonConvert.DeserializeObject<ROOM>(jObject.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ROOMs.Add(result);

            try
            {
                await db.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {

                if (LocationExists(result.RoomID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }

            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [ResponseType(typeof(ROOM))]
        public async Task<IHttpActionResult> FindRoom(string id)
        {
            var room = db.ROOMs.FindAsync(id);
            await room;
            if (room == null)
            {
                return NotFound();
            }
            return Json(room);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> EditRoom(string id, JObject jObject)
        {
            ROOM rOOM = JsonConvert.DeserializeObject<ROOM>(jObject.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rOOM.RoomID)
            {
                return BadRequest();
            }

            db.Entry(rOOM).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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
        [ResponseType(typeof(ROOM))]
        public async Task<IHttpActionResult> DeleteRoom(string id)
        {
            ROOM rOOM = await db.ROOMs.FindAsync(id);
            if (rOOM == null)
            {
                return NotFound();
            }

            db.ROOMs.Remove(rOOM);
            await db.SaveChangesAsync();

            return Ok(rOOM);
        }
        private bool CinemaExists(string id)
        {
            return db.CINEMAs.Count(e => e.CinemaID == id) > 0;
        }

        private bool LocationExists(string id)
        {
            return db.CINEMA_LOCATION.Count(e => e.LocationID == id) > 0;
        }
        private bool RoomExists(string id)
        {
            return db.ROOMs.Count(e => e.RoomID == id) > 0;
        }
    }
}
