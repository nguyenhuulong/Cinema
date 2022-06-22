
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;

namespace CinemaAPI.Controllers
{
    public class SearchAPIController : ApiController
    {
        CinemaDB db = new CinemaDB();

        //FilmCategory
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> FilmCategory()
        {
            var result = db.Database.SqlQuery<string>($"exec FilmCategory");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        //FilmNation
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> FilmNation()
        {
            var result = db.Database.SqlQuery<string>($"exec FilmNation");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }


        //SearchFilmByName
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{Name}")]
        public async Task<IHttpActionResult> SearchFilmByName([FromRoute] string Name)
        {
            var result = db.Database.SqlQuery<MOVIE>($"exec SearchFilm @name=N'{Name}'");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        //SearchFilmByNameAndNation
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{Name}/{Nation}")]
        public async Task<IHttpActionResult> SearchFilmByNameAndNation([FromRoute] string Name, [FromRoute] string nation)
        {
            var result = db.Database.SqlQuery<MOVIE>($"exec SearchFilm_Nation @name=N'{Name}, @nation = N'{nation}'");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        //SearchFilmByNameAndCategory
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{Name}/{Nation}")]
        public async Task<IHttpActionResult> SearchFilmByNameAndCategory([FromRoute] string Name, [FromRoute] string Category)
        {
            var result = db.Database.SqlQuery<MOVIE>($"exec SearchFilm_Category @name=N'{Name}, @category = N'{Category}'");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        //SearchFilmByNameAndNation_Category
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{Name}/{Nation}")]
        public async Task<IHttpActionResult> SearchFilmByNameAndNation_Category([FromRoute] string Name, [FromRoute] string Nation, [FromRoute] string Category)
        {
            var result = db.Database.SqlQuery<MOVIE>($"exec SearchFilm_Category @name=N'{Name}, @nation = N'{Nation}', @category=N'{Category}");
            await result.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }
    }
}