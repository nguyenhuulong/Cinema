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
    public class TicketController : ApiController
    {
        private CinemaDB db = new CinemaDB();

        // GET: api/Ticket
        public IQueryable<TICKET> GetTICKETs()
        {
            return db.TICKETs;
        }

        // GET: api/Ticket/5
        [ResponseType(typeof(TICKET))]
        public async Task<IHttpActionResult> GetTICKET(string id)
        {
            TICKET tICKET = await db.TICKETs.FindAsync(id);
            if (tICKET == null)
            {
                return NotFound();
            }

            return Ok(tICKET);
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