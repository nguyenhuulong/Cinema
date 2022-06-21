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
using Microsoft.AspNetCore.Mvc;

namespace CinemaAPI.Controllers
{
    public class TicketController : ApiController
    {
        private CinemaDB db = new CinemaDB();

        // GET: api/Ticket
        public IQueryable<TICKET> GetTICKETs()
        {
            return db.TICKETs;
        }

        //GetCinemaFromName
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> GetCinemaFromName(string cinemaname)
        {
            var cinema = db.Database.SqlQuery<CINEMA>($"exec GetCinemaFromName N'{cinemaname}'");
            await cinema.ToListAsync();
            if (cinema == null)
            {
                return NotFound();
            }

            return Json(cinema);
        }

        //GetFilmFromName
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> GetFilmFromName(string filmname)
        {
            var film = db.Database.SqlQuery<MOVIE>($"exec GetFilmFromName N'{filmname}'");
            await film.ToListAsync();
            if (film == null)
            {
                return NotFound();
            }

            return Json(film);
        }

        //GetRoomFromID
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> GetRoomFromID(string ID)
        {
            var room = db.Database.SqlQuery<MOVIE>($"exec GetFilmFromName N'{ID}'");
            await room.ToListAsync();
            if (room == null)
            {
                return NotFound();
            }

            return Json(room);
        }

        //GetTicketType
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> GetAllTicketType()
        {
            var ticket_type = db.TICKET_TYPE;
            await ticket_type.ToListAsync();
            if (ticket_type == null)
            {
                return NotFound();
            }

            return Json(ticket_type);
        }

        //GetListService
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> GetListService()
        {
            var service = db.SERVICEs;
            await service.ToListAsync();
            if (service == null)
            {
                return NotFound();
            }

            return Json(service);
        }
        //GetListSeatBooked
        [System.Web.Http.HttpGet] 
        [Microsoft.AspNetCore.Mvc.Route("{MovieName}/{Room}/{ShowTime}")]
        public async Task<IHttpActionResult> GetListSeatBooked([FromRoute] string MovieName,[FromRoute] string Room,[FromRoute] string ShowTime)
        {
            var seat = db.Database.SqlQuery<string>($"exec GetListSeatBooked N'{MovieName}', '{Room}', '{ShowTime}', N'2021-10-25'");
            await seat.ToListAsync();
            if (seat == null)
            {
                return NotFound();
            }

            return Json(seat);
        }
        // GET: api/Ticket/5
        [ResponseType(typeof(TICKET_TYPE))]
        public async Task<IHttpActionResult> GetTicketType(string id)
        {
            TICKET_TYPE tICKET = await db.TICKET_TYPE.FindAsync(id);
            if (tICKET == null)
            {
                return NotFound();
            }

            return Ok(tICKET);
        }
        [ResponseType(typeof(SERVICE))]
        public async Task<IHttpActionResult> GetServiceType(string id)
        {
            SERVICE sERVICE = await db.SERVICEs.FindAsync(id);
            if (sERVICE == null)
            {
                return NotFound();
            }

            return Ok(sERVICE);
        }

        // PUT: api/Ticket/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTICKET(string id, TICKET tICKET)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tICKET.TicketID)
            {
                return BadRequest();
            }

            db.Entry(tICKET).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TICKETExists(id))
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

        // POST: api/Ticket
        [ResponseType(typeof(TICKET))]
        public async Task<IHttpActionResult> PostTICKET(TICKET tICKET)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TICKETs.Add(tICKET);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TICKETExists(tICKET.TicketID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tICKET.TicketID }, tICKET);
        }

        // DELETE: api/Ticket/5
        [ResponseType(typeof(TICKET))]
        public async Task<IHttpActionResult> DeleteTICKET(string id)
        {
            TICKET tICKET = await db.TICKETs.FindAsync(id);
            if (tICKET == null)
            {
                return NotFound();
            }

            db.TICKETs.Remove(tICKET);
            await db.SaveChangesAsync();

            return Ok(tICKET);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TICKETExists(string id)
        {
            return db.TICKETs.Count(e => e.TicketID == id) > 0;
        }
    }
}