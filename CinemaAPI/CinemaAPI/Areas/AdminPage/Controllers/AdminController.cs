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

namespace CinemaAPI.Areas.AdminPage.Controllers
{
    public class AdminController : ApiController
    {
        private CinemaDB db = new CinemaDB();

        // GET: api/location
        public IQueryable<CINEMA_LOCATION> GetLOCATIONs()
        {
            return db.CINEMA_LOCATION;
        }

        // GET: api/
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

        [HttpGet]
        [ResponseType(typeof(AdminInfo))]
        public async Task<IHttpActionResult> GetAdminInfo()
        {
            var admins = db.Database.SqlQuery<AdminInfo>("exec GetAdminInfo");
            await admins.ToListAsync();
            if (admins == null)
            {
                return NotFound();
            }
            return Json(admins);
        }

        [HttpGet]
        public IQueryable<DEPARTMENT> GetDepartment()
        {
            return db.DEPARTMENTs;
        }

        public IQueryable<POST> GetPost()
        {
            return db.POSTs;
        }

        [HttpGet]
        [ResponseType(typeof(MovieItem))]
        public async Task<IHttpActionResult> GetAllBlog()
        {
            var blogs = db.Database.SqlQuery<MovieItem>("exec GetAllBlog");
            await blogs.ToListAsync();
            if (blogs == null)
            {
                return NotFound();
            }
            return Json(blogs);
        }

        [HttpGet]
        [ResponseType(typeof(MovieItem))]
        public async Task<IHttpActionResult> GetAllReview()
        {
            var reviews = db.Database.SqlQuery<MovieItem>("exec GetAllReview");
            await reviews.ToListAsync();
            if (reviews == null)
            {
                return NotFound();
            }
            return Json(reviews);
        }

        [HttpGet]
        [ResponseType(typeof(MovieItem))]
        public async Task<IHttpActionResult> GetAllSale()
        {
            var sales = db.Database.SqlQuery<MovieItem>("exec GetAllSale");
            await sales.ToListAsync();
            if (sales == null)
            {
                return NotFound();
            }
            return Json(sales);
        }

        [HttpGet]
        [ResponseType(typeof(FeedbackList))]
        public async Task<IHttpActionResult> GetFeedBack()
        {
            var fbs = db.Database.SqlQuery<MovieItem>("exec GetFeedBack");
            await fbs.ToListAsync();
            if (fbs == null)
            {
                return NotFound();
            }
            return Json(fbs);
        }

        [HttpGet]
        [ResponseType(typeof(MovieItem))]
        public async Task<IHttpActionResult> GetMovieInfo()
        {
            var movs = db.Database.SqlQuery<MovieItem>("exec GetMovieInfo");
            await movs.ToListAsync();
            if (movs == null)
            {
                return NotFound();
            }
            return Json(movs);
        }

        [HttpGet]
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> GetCurrentFilm()
        {
            var mOVIE = db.Database.SqlQuery<MOVIE>("exec GetCurrentFilm");
            await mOVIE.ToListAsync();
            if (mOVIE == null)
            {
                return NotFound();
            }
            return Json(mOVIE);
        }

        [HttpGet]
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> GetFutureFilm()
        {
            var mOVIE = db.Database.SqlQuery<MOVIE>("exec GetFutureFilm");
            await mOVIE.ToListAsync();
            if (mOVIE == null)
            {
                return NotFound();
            }
            return Json(mOVIE);
        }

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
    }
}
