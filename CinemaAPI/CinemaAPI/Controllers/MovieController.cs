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
    public class MovieController : ApiController
    {
        private CinemaDB db = new CinemaDB();

        // GET: api/Movies
        public IQueryable<MOVIE> GetMOVIEs()
        {
            return db.MOVIEs;
        }

        // GET: api/Movies/5
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> Details(string id)
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
        //MovieFuture
        [HttpGet]
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> MovieFuture()
        {
            var mOVIE = db.Database.SqlQuery<MOVIE>("exec GetFutureFilm");
            await mOVIE.ToListAsync();
            if (mOVIE == null)
            {
                return NotFound();
            }

            return Json(mOVIE);
        }
       
    }
}