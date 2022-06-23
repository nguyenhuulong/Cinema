using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;
using System.Web.Http;
using System.Data.Entity;

namespace CinemaAPI.Controllers
{
    public class HomepageAPIController : ApiController
    {
        CinemaDB db = new CinemaDB();
        //GetReview
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(POST))]
        public async Task<IHttpActionResult> GetReview()
        {
            var result = db.Database.SqlQuery<POST>($"exec GetReview");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        //GetBlog
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(POST))]
        public async Task<IHttpActionResult> GetBlog()
        {
            var result = db.Database.SqlQuery<POST>($"exec GetBlog");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        //GetSaleNew
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(POST))]
        public async Task<IHttpActionResult> GetSaleNew()
        {
            var result = db.Database.SqlQuery<POST>($"exec SaleNew");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }
        //GetLocation
        [System.Web.Http.HttpGet]
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
        //GetListSlide
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(AD))]
        public async Task<IHttpActionResult> GetListSlide()
        {
            var result = db.Database.SqlQuery<AD>($"exec GetListSlide");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }
    }
}