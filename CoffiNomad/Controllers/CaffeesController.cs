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
using CoffiNomad;

namespace CoffiNomad.Controllers
{
    public class CaffeesController : ApiController
    {
        private CoffiContext db = new CoffiContext();

        // GET: api/Caffees
        public IQueryable<Caffee> GetCaffees()
        {
            return db.Caffees;
        }

        // GET: api/Caffees/5
        [ResponseType(typeof(Caffee))]
        public async Task<IHttpActionResult> GetCaffee(int id)
        {
            Caffee caffee = await db.Caffees.FindAsync(id);
            if (caffee == null)
            {
                return NotFound();
            }

            return Ok(caffee);
        }

        // PUT: api/Caffees/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCaffee(int id, Caffee caffee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != caffee.CaffeeID)
            {
                return BadRequest();
            }

            db.Entry(caffee).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaffeeExists(id))
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

        // POST: api/Caffees
        [ResponseType(typeof(Caffee))]
        public async Task<IHttpActionResult> PostCaffee(Caffee caffee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Caffees.Add(caffee);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CaffeeExists(caffee.CaffeeID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = caffee.CaffeeID }, caffee);
        }

        // DELETE: api/Caffees/5
        [ResponseType(typeof(Caffee))]
        public async Task<IHttpActionResult> DeleteCaffee(int id)
        {
            Caffee caffee = await db.Caffees.FindAsync(id);
            if (caffee == null)
            {
                return NotFound();
            }

            db.Caffees.Remove(caffee);
            await db.SaveChangesAsync();

            return Ok(caffee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CaffeeExists(int id)
        {
            return db.Caffees.Count(e => e.CaffeeID == id) > 0;
        }
    }
}