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
    public class LocatiesController : ApiController
    {
        private CoffiContext db = new CoffiContext();

        // GET: api/Locaties
        public IQueryable<Locatie> GetLocaties()
        {
            return db.Locaties;
        }

        // GET: api/Locaties/5
        [ResponseType(typeof(Locatie))]
        public async Task<IHttpActionResult> GetLocatie(int id)
        {
            Locatie locatie = await db.Locaties.FindAsync(id);

            db.Entry(locatie)
                .Collection(l => l.Caffees)
                .Load();

            if (locatie == null)
            {
                return NotFound();
            }

            return Ok(locatie);
        }

        // PUT: api/Locaties/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLocatie(int id, Locatie locatie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locatie.LocatieID)
            {
                return BadRequest();
            }

            db.Entry(locatie).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocatieExists(id))
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

        // POST: api/Locaties
        [ResponseType(typeof(Locatie))]
        public async Task<IHttpActionResult> PostLocatie(Locatie locatie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Locaties.Add(locatie);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LocatieExists(locatie.LocatieID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = locatie.LocatieID }, locatie);
        }

        // DELETE: api/Locaties/5
        [ResponseType(typeof(Locatie))]
        public async Task<IHttpActionResult> DeleteLocatie(int id)
        {
            Locatie locatie = await db.Locaties.FindAsync(id);
            if (locatie == null)
            {
                return NotFound();
            }

            db.Locaties.Remove(locatie);
            await db.SaveChangesAsync();

            return Ok(locatie);
        }

        private bool LocatieExists(int id)
        {
            return db.Locaties.Count(e => e.LocatieID == id) > 0;
        }
    }
}