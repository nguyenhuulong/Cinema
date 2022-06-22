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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CinemaAPI.Controllers
{
    public class TicketAPIController : ApiController
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
        [ResponseType(typeof(ROOM))]
        public async Task<IHttpActionResult> GetRoomFromID(string ID)
        {
            var room = db.Database.SqlQuery<ROOM>($"exec GetRoomById N'{ID}'");
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

        //GetMaxSeatID
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(String))]
        public async Task<IHttpActionResult> GetMaxSeatID()
        {
            var seat = db.Database.SqlQuery<String>("exec GetMaxSeatID");
            await seat.ToListAsync();

            if (seat == null)
            {
                return NotFound();
            }

            return Json(seat.ElementAt(0));
        }

        //GetMaxTicketID
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(String))]
        public async Task<IHttpActionResult> GetMaxTicketID()
        {
            var seat = db.Database.SqlQuery<String>("exec GetMaxTicketID");
            await seat.ToListAsync();

            if (seat == null)
            {
                return NotFound();
            }

            return Json(seat.ElementAt(0));
        }

        //GetMaxSVCID

        [System.Web.Http.HttpGet]
        [ResponseType(typeof(String))]
        public async Task<IHttpActionResult> GetMaxSVCID()
        {
            var seat = db.Database.SqlQuery<String>("exec GetMaxSVCID");
            await seat.ToListAsync();

            if (seat == null)
            {
                return NotFound();
            }

            return Json(seat.ElementAt(0));
        }

        //GetMaxBillID
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(String))]
        public async Task<IHttpActionResult> GetMaxBillID()
        {
            var seat = db.Database.SqlQuery<String>("exec GetMaxBillID");
            await seat.ToListAsync();

            if (seat == null)
            {
                return NotFound();
            }

            return Json(seat.ElementAt(0));
        }
        //GetListSeatBooked
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{MovieName}/{Room}/{ShowTime}")]
        public async Task<IHttpActionResult> GetListSeatBooked([FromRoute] string MovieName, [FromRoute] string Room, [FromRoute] string ShowTime)
        {
            var seat = db.Database.SqlQuery<Seat>($"exec GetListSeatBooked N'{MovieName}', '{Room}', '{ShowTime}', N'2021-10-25'");
            await seat.ToListAsync();
            if (seat == null)
            {
                return NotFound();
            }

            return Json(seat);
        }
        // GET: api/Ticket/5
        // GetTicketType
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

        //FindMovieTime
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> FindMovieTime(string id)
        {
            MOVIE_TIME tICKET = await db.MOVIE_TIME.FindAsync(id);
            if (tICKET == null)
            {
                return NotFound();
            }

            return Ok(tICKET.MovieTimeID);
        }

        //FindServiceName

        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> FindServiceName(string id)
        {
            SERVICE tICKET = await db.SERVICEs.FindAsync(id);
            if (tICKET == null)
            {
                return NotFound();
            }

            return Ok(tICKET.ServiceName);
        }

        //FindServicePrice
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> FindServicePrice(string id)
        {
            SERVICE tICKET = await db.SERVICEs.FindAsync(id);
            if (tICKET == null)
            {
                return NotFound();
            }

            return Ok(tICKET.ServicePrice);
        }
        //CheckVoucher
        [ResponseType(typeof(DISCOUNT_CODE))]
        public async Task<IHttpActionResult> CheckVoucher(string id)
        {
            DISCOUNT_CODE dISCOUNT = await db.DISCOUNT_CODE.FindAsync(id);
            if (dISCOUNT == null)
            {
                return NotFound();
            }

            return Ok(dISCOUNT);
        }

        //GetServiceById
        [ResponseType(typeof(SERVICE))]

        public async Task<IHttpActionResult> GetService(string id)
        {
            SERVICE sERVICE = await db.SERVICEs.FindAsync(id);
            if (sERVICE == null)
            {
                return NotFound();
            }

            return Ok(sERVICE);
        }



        //GetServiceToCashById
        [ResponseType(typeof(SERVICE_TO_CASH))]
        public async Task<IHttpActionResult> GetServiceToCash(string id)
        {
            SERVICE_TO_CASH sERVICE = await db.SERVICE_TO_CASH.FindAsync(id);
            if (sERVICE == null)
            {
                return NotFound();
            }

            return Ok(sERVICE);
        }


        // AddServiceToCash
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(SERVICE_TO_CASH))]
        public async Task<IHttpActionResult> AddServiceToCash(JObject sERVICE)
        {
            SERVICE_TO_CASH result = JsonConvert.DeserializeObject<SERVICE_TO_CASH>(sERVICE.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.SERVICE_TO_CASH.Add(result);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (SVTCExists(result.ServiceToCashID))
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

        // Order
        // AddSeat
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(SERVICE_TO_CASH))]
        public async Task<IHttpActionResult> AddSeat(JObject jObject)
        {
            SEAT result = JsonConvert.DeserializeObject<SEAT>(jObject.ToString());
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            db.SEATs.Add(result);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (SEATExists(result.SeatID))
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

        // POST: api/Ticket
        // AddTicket
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(TICKET))]

        public async Task<IHttpActionResult> AddTicket(JObject jObject)
        {
            TICKET result = JsonConvert.DeserializeObject<TICKET>(jObject.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            db.TICKETs.Add(result);

            try
            {
                await db.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {

                if (TICKETExists(result.TicketID))
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

        // DELETE: api/Ticket/5
        //Add BIll
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(TICKET))]

        public async Task<IHttpActionResult> AddBill(JObject jObject)
        {


            BILL result = JsonConvert.DeserializeObject<BILL>(jObject.ToString());
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            db.BILLs.Add(result);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (BillExists(result.BillID))
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
        private bool SEATExists(string id)
        {
            return db.SEATs.Count(e => e.SeatID == id) > 0;
        }
        private bool BillExists(string id)
        {
            return db.BILLs.Count(e => e.BillID == id) > 0;
        }
        private bool SVTCExists(string id)
        {
            return db.SERVICE_TO_CASH.Count(e => e.ServiceToCashID == id) > 0;
        }
    }
}