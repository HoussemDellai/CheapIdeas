using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CheapIdeas.Web.Models;
using CheapIdeas.Web.Models.Entities;
using Microsoft.AspNet.Identity;

namespace CheapIdeas.Web.Controllers
{
    //[Authorize]
    [RoutePrefix("api/ideas")]
    public class IdeasController : ApiController
    {
        private IdeasContext db = new IdeasContext();

        // GET: api/Ideas/ForCurrentUser
        [Authorize]
        [HttpGet]
        [Route("ForCurrentUser")]
        public IQueryable<Idea> GetIdeasForCurrentUser()
        {
            var userId = User.Identity.GetUserId();

            var ideas = db.Ideas.Where(idea => idea.UserId == userId);

            return ideas;
        }

        // GET: api/Ideas
        [Authorize]
        [HttpGet]
        [Route("all")]
        public IQueryable<Idea> GetIdeas()
        {
            return db.Ideas;
        }

        // GET: api/Ideas/byId/5
        [Authorize]
        [HttpGet]
        [Route("byId")]
        [ResponseType(typeof(Idea))]
        public IHttpActionResult GetIdea(int id)
        {
            Idea idea = db.Ideas.Find(id);
            if (idea == null)
            {
                return NotFound();
            }

            return Ok(idea);
        }

        // PUT: api/Ideas/5
        [Authorize]
        [HttpPut]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIdea(int id, Idea idea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != idea.Id)
            {
                return BadRequest();
            }

            db.Entry(idea).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdeaExists(id))
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

        // POST: api/Ideas/add
        [Authorize]
        [HttpPost]
        [Route("add")]
        [ResponseType(typeof(Idea))]
        public IHttpActionResult PostIdea(Idea idea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId();
            idea.UserId = userId;
            idea.UserName = User.Identity.Name;

            db.Ideas.Add(idea);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = idea.Id }, idea);
        }

        // DELETE: api/Ideas/delete/5
        [Authorize]
        [HttpDelete]
        [Route("delete")]
        [ResponseType(typeof(Idea))]
        public IHttpActionResult DeleteIdea(int id)
        {
            Idea idea = db.Ideas.Find(id);
            if (idea == null)
            {
                return NotFound();
            }

            db.Ideas.Remove(idea);
            db.SaveChanges();

            return Ok(idea);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IdeaExists(int id)
        {
            return db.Ideas.Count(e => e.Id == id) > 0;
        }
    }
}