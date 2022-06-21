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
    public class CinematicController : ApiController
    {
        private CinemaDB db = new CinemaDB();

        [HttpGet]
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> CurMovie()
        {
            var curMovies = db.Database.SqlQuery<MOVIE>("exec CurMovie");
            await curMovies.ToListAsync();
            if (curMovies == null)
            {
                return NotFound();
            }

            return Json(curMovies);
        }

        [HttpGet]
        [ResponseType(typeof(MOVIE))]
        public async Task<IHttpActionResult> GetPostInfoByID(string id)
        {
            POST post = await db.POSTs.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Json(post);
        }

        [HttpGet]
        [ResponseType(typeof(POST))]
        public async Task<IHttpActionResult> GetPostInfo()
        {
            var reviews = db.Database.SqlQuery<POST>("exec GetPostInfo");
            await reviews.ToListAsync();
            if (reviews == null)
            {
                return NotFound();
            }

            return Json(reviews);
        }

        [HttpGet]
        [ResponseType(typeof(POST))]
        public async Task<IHttpActionResult> GetAllReview()
        {
            var reviews = db.Database.SqlQuery<POST>("exec GetAllReview");
            await reviews.ToListAsync();
            if (reviews == null)
            {
                return NotFound();
            }

            return Json(reviews);
        }

        [HttpGet]
        [ResponseType(typeof(POST))]
        public async Task<IHttpActionResult> GetAllBlog()
        {
            var blogs = db.Database.SqlQuery<POST>("exec GetAllBlog");
            await blogs.ToListAsync();
            if (blogs == null)
            {
                return NotFound();
            }

            return Json(blogs);
        }

        [HttpGet]
        [ResponseType(typeof(POST))]
        public async Task<IHttpActionResult> GetAllSale()
        {
            var sales = db.Database.SqlQuery<POST>("exec GetAllSale");
            await sales.ToListAsync();
            if (sales == null)
            {
                return NotFound();
            }

            return Json(sales);
        }

        [HttpGet]
        [ResponseType(typeof(POST))]
        public async Task<IHttpActionResult> GetReview()
        {
            var reviews = db.Database.SqlQuery<POST>("exec GetReview");
            await reviews.ToListAsync();
            if (reviews == null)
            {
                return NotFound();
            }

            return Json(reviews);
        }

        [HttpGet]
        [ResponseType(typeof(POST))]
        public async Task<IHttpActionResult> GetBlog()
        {
            var blogs = db.Database.SqlQuery<POST>("exec GetBlog");
            await blogs.ToListAsync();
            if (blogs == null)
            {
                return NotFound();
            }

            return Json(blogs);
        }

        [HttpGet]
        [ResponseType(typeof(POST))]
        public async Task<IHttpActionResult> GetSaleNew()
        {
            var sales = db.Database.SqlQuery<POST>("exec GetSaleNew");
            await sales.ToListAsync();
            if (sales == null)
            {
                return NotFound();
            }

            return Json(sales);
        }

        [HttpGet]
        [ResponseType(typeof(POST_CONTENT))]
        public async Task<IHttpActionResult> GetPostContentFromPostID(string id)
        {
            var postContent = db.Database.SqlQuery<POST_CONTENT>($"exec GetPostContentFromPostID {id}");
            await postContent.ToListAsync();
            if (postContent == null)
            {
                return NotFound();
            }

            return Json(postContent);
        }

        [HttpGet]
        [ResponseType(typeof(Post_Category))]
        public async Task<IHttpActionResult> GetCategoryFromPost(string id)
        {
            var postCategory = db.Database.SqlQuery<Post_Category>($"exec GetCategoryFromPost {id}");
            await postCategory.ToListAsync();
            if (postCategory == null)
            {
                return NotFound();
            }

            return Json(postCategory);
        }
    }
}
