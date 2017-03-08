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
        public async Task<IHttpActionResult> PutCaffee(int caffeeId, string naam, string straat, int locatieID)
        {
            Caffee caffee = db.Caffees.Find(caffeeId);
            Locatie locatie = db.Locaties.Find(locatieID);

            if (locatie != null && caffee != null)
            {
                if (!string.IsNullOrEmpty(naam) && !string.IsNullOrEmpty(straat))
                {
                    caffee.Locatie = locatie;
                    caffee.Name = naam;
                    caffee.Straat = straat;

                    db.Entry(caffee).State = EntityState.Modified;
                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CaffeeExists(caffeeId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                else
                {
                    return BadRequest();
                }    

            }
            else {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
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
        public async Task<IHttpActionResult> PostCaffee(string naam, string straat, int locatieID)
        {
            Caffee caffee = null;
            Locatie locatie = db.Locaties.Find(locatieID);
            if (locatie != null)
            {
                if (!string.IsNullOrEmpty(naam) && !string.IsNullOrEmpty(straat))
                {

                    caffee = new Caffee
                    {
                        Name = naam,
                        Straat = straat,
                        LocatieID = locatieID,
                        Locatie = locatie
                    };

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
                }
            }
            else
            {
                return BadRequest();
            }
         
            return CreatedAtRoute("DefaultApi", new { id = caffee.CaffeeID }, caffee);
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

        // GET: api/caffees/1/1/
        [Route("api/caffees/{locatieID:int}/{categoryID:int}")]
        public IEnumerable<Caffee> GetCaffees(int locatieID, int categoryID)
        {

            var caffees = from caffee in db.Caffees
                          where db.Beoordeling
                          .Select(be => be.CaffeeID)
                          .Contains(caffee.CaffeeID)
                          select caffee;

            caffees = from caffee in caffees
                      where db.Beoordeling
                      .Select(be => be.CategoryID)
                      .Contains(categoryID)
                      select caffee;

            caffees = from caffee in caffees
                      where caffee.LocatieID == locatieID
                      select caffee;
           
            foreach (var c in caffees)
            {
                var beoordelingen = from beoordeling in db.Beoordeling
                where beoordeling.CategoryID == categoryID
                where beoordeling.CaffeeID == c.CaffeeID
                select beoordeling;
                c.Beoordelingen = beoordelingen.ToList();
               
            }

            return caffees;
        }

        private bool CaffeeExists(int id)
        {
            return db.Caffees.Count(e => e.CaffeeID == id) > 0;
        }


    }
}