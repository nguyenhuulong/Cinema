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
    public class BusinessController : ApiController
    {
        private CinemaDB db = new CinemaDB();

        [HttpGet]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> GetPayment()
        {
            var payment = db.Database.SqlQuery<int>($"Select Count(BillID) from BILL");
            await payment.ToListAsync();
            if (payment == null)
            {
                return NotFound();
            }
            return Json(payment);
        }

        public IQueryable<TICKET_TYPE> GetTicketTypes()
        {
            return db.TICKET_TYPE;
        }

        public IQueryable<SERVICE> GetServices()
        {
            return db.SERVICEs;
        }

        public IQueryable<DISCOUNT_CODE> GetDiscount()
        {
            return db.DISCOUNT_CODE;
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

        public IQueryable<BILL> GetBill()
        {
            return db.BILLs;
        }

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

        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> EditTICKET(string id, JObject result)
        {
            TICKET tICKET = JsonConvert.DeserializeObject<TICKET>(result.ToString());
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

        [HttpDelete]
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

        [HttpPost]
        [ResponseType(typeof(SERVICE))]
        public async Task<IHttpActionResult> AddSERVICE(JObject jObject)
        {
            SERVICE result = JsonConvert.DeserializeObject<SERVICE>(jObject.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SERVICEs.Add(result);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SERVICEExists(result.ServiceID))
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

        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> EditService(string id, JObject result)
        {
            SERVICE sERVICE = JsonConvert.DeserializeObject<SERVICE>(result.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sERVICE.ServiceID)
            {
                return BadRequest();
            }

            db.Entry(sERVICE).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SERVICEExists(id))
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
        [ResponseType(typeof(SERVICE))]
        public async Task<IHttpActionResult> DeleteService(string id)
        {
            SERVICE sERVICE = await db.SERVICEs.FindAsync(id);
            if (sERVICE == null)
            {
                return NotFound();
            }

            db.SERVICEs.Remove(sERVICE);
            await db.SaveChangesAsync();

            return Ok(sERVICE);
        }

        [HttpPost]
        [ResponseType(typeof(DISCOUNT_CODE))]
        public async Task<IHttpActionResult> AddDiscount(JObject jObject)
        {
            DISCOUNT_CODE dISCOUNT = JsonConvert.DeserializeObject<DISCOUNT_CODE>(jObject.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DISCOUNT_CODE.Add(dISCOUNT);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DiscountExists(dISCOUNT.CodeID))
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
        [ResponseType(typeof(DISCOUNT_CODE))]
        public async Task<IHttpActionResult> GetDiscount(string id)
        {
            DISCOUNT_CODE dISCOUNT = await db.DISCOUNT_CODE.FindAsync(id);
            if (dISCOUNT == null)
            {
                return NotFound();
            }

            return Ok(dISCOUNT);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> EditDiscount(string id, JObject jObject)
        {
            DISCOUNT_CODE dISCOUNT = JsonConvert.DeserializeObject<DISCOUNT_CODE>(jObject.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dISCOUNT.CodeID)
            {
                return BadRequest();
            }

            db.Entry(dISCOUNT).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountExists(id))
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
        [ResponseType(typeof(DISCOUNT_CODE))]
        public async Task<IHttpActionResult> DeleteDiscount(string id)
        {
            DISCOUNT_CODE dISCOUNT = await db.DISCOUNT_CODE.FindAsync(id);
            if (dISCOUNT == null)
            {
                return NotFound();
            }

            db.DISCOUNT_CODE.Remove(dISCOUNT);
            await db.SaveChangesAsync();

            return Ok(dISCOUNT);
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

        private bool SERVICEExists(string id)
        {
            return db.SERVICEs.Count(e => e.ServiceID == id) > 0;
        }

        private bool DiscountExists(string id)
        {
            return db.DISCOUNT_CODE.Count(e => e.CodeID == id) > 0;
        }
    }
}
