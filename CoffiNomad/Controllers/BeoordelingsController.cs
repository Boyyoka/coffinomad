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
using System.Web.Http.Cors;

namespace CoffiNomad.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BeoordelingsController : ApiController
    {
        private CoffiContext db = new CoffiContext();

        // GET: api/Beoordelings
        public IQueryable<Beoordeling> GetBeoordeling()
        {
            return db.Beoordeling;
        }

        // GET: api/Beoordelings/5
        [ResponseType(typeof(Beoordeling))]
        public async Task<IHttpActionResult> GetBeoordeling(int id)
        {
            Beoordeling beoordeling = await db.Beoordeling.FindAsync(id);
            if (beoordeling == null)
            {
                return NotFound();
            }

            return Ok(beoordeling);
        }

        // PUT: api/Beoordelings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBeoordeling(int id, Beoordeling beoordeling)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != beoordeling.CategoryID)
            {
                return BadRequest();
            }

            db.Entry(beoordeling).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeoordelingExists(id))
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

        // POST: api/Beoordelings
        [ResponseType(typeof(Beoordeling))]
        public async Task<IHttpActionResult> PostBeoordeling(Beoordeling beoordeling)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Beoordeling.Add(beoordeling);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BeoordelingExists(beoordeling.CategoryID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = beoordeling.CategoryID }, beoordeling);
        }

        // DELETE: api/Beoordelings/5
        [ResponseType(typeof(Beoordeling))]
        public async Task<IHttpActionResult> DeleteBeoordeling(int id)
        {
            Beoordeling beoordeling = await db.Beoordeling.FindAsync(id);
            if (beoordeling == null)
            {
                return NotFound();
            }

            db.Beoordeling.Remove(beoordeling);
            await db.SaveChangesAsync();

            return Ok(beoordeling);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BeoordelingExists(int id)
        {
            return db.Beoordeling.Count(e => e.CategoryID == id) > 0;
        }
    }
}