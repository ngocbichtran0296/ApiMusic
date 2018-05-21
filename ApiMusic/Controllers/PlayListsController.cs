using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ApiMusic.Entities;

namespace ApiMusic.Controllers
{
    public class PlayListsController : ApiController
    {
        public PlayListsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        private ApiMusicDatabaseEntities db = new ApiMusicDatabaseEntities();

        // GET: api/PlayLists
        public IQueryable<PlayList> GetPlayLists()
        {
            return db.PlayLists;
        }

        // GET: api/PlayLists/5
        [ResponseType(typeof(PlayList))]
        public IHttpActionResult GetPlayList(int id)
        {
            PlayList playList = db.PlayLists.Find(id);
            if (playList == null)
            {
                return NotFound();
            }

            return Ok(playList);
        }

        // PUT: api/PlayLists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlayList(int id, PlayList playList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != playList.PlayListId)
            {
                return BadRequest();
            }

            db.Entry(playList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayListExists(id))
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

        // POST: api/PlayLists
        [ResponseType(typeof(PlayList))]
        public IHttpActionResult PostPlayList(PlayList playList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PlayLists.Add(playList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = playList.PlayListId }, playList);
        }

        // DELETE: api/PlayLists/5
        [ResponseType(typeof(PlayList))]
        public IHttpActionResult DeletePlayList(int id)
        {
            PlayList playList = db.PlayLists.Find(id);
            if (playList == null)
            {
                return NotFound();
            }

            db.PlayLists.Remove(playList);
            db.SaveChanges();

            return Ok(playList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayListExists(int id)
        {
            return db.PlayLists.Count(e => e.PlayListId == id) > 0;
        }
    }
}