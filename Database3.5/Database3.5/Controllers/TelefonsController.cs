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
using Database3._5.Models;

namespace Database3._5.Controllers
{
    public class TelefonsController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/Telefons
        public List<TelefonDTO> GetTelefons()
        {
            var query = from a in db.Telefons.Include(p => p.hars) select a;

            List<TelefonDTO> dtolist = new List<TelefonDTO>();

            foreach (var telefon in query)
            {
                var dto = new TelefonDTO
                {
                    TelefonNummerID = telefon.TelefonNummerID,
                    People = new List<PersonRefAdresse>()
                };

                foreach (var person in telefon.hars)
                {
                    dto.People.Add(new PersonRefAdresse
                    {
                        Navn = person.Person.Fornavn,
                        Personnummer = person.Person.PersonNummer.ToString()
                    });
                }
                dtolist.Add(dto);
            }

            return dtolist;
        }

        // GET: api/Telefons/5
        [ResponseType(typeof(Telefon))]
        public async Task<IHttpActionResult> GetTelefon(long id)
        {
            Telefon telefon = await db.Telefons.Include(p => p.hars).SingleOrDefaultAsync(a => a.TelefonNummerID == id);
            if (telefon == null)
            {
                return NotFound();
            }
            var dto = new TelefonDTO
            {
                TelefonNummerID = telefon.TelefonNummerID,
                People = new List<PersonRefAdresse>()
            };

            foreach (var person in telefon.hars)
            {
                dto.People.Add(new PersonRefAdresse
                {
                    Navn = person.Person.Fornavn,
                    Personnummer = person.Person.PersonNummer.ToString()
                });
            }


            return Ok(dto);
        }

        // PUT: api/Telefons/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTelefon(long id, Telefon telefon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != telefon.TelefonNummerID)
            {
                return BadRequest();
            }

            db.Entry(telefon).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefonExists(id))
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

        // POST: api/Telefons
        [ResponseType(typeof(Telefon))]
        public async Task<IHttpActionResult> PostTelefon(Telefon telefon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Telefons.Add(telefon);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TelefonExists(telefon.TelefonNummerID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = telefon.TelefonNummerID }, telefon);
        }

        // DELETE: api/Telefons/5
        [ResponseType(typeof(Telefon))]
        public async Task<IHttpActionResult> DeleteTelefon(long id)
        {
            Telefon telefon = await db.Telefons.FindAsync(id);
            if (telefon == null)
            {
                return NotFound();
            }
            var har = await db.hars.SingleOrDefaultAsync(h => h.TelefonNummerID == telefon.TelefonNummerID);
            if (har == null)
            {
                return NotFound();
            }
            db.Telefons.Remove(telefon);
            db.hars.Remove(har);
            await db.SaveChangesAsync();

            return Ok(new TelefonDTO {People = null,TelefonNummerID = telefon.TelefonNummerID});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TelefonExists(long id)
        {
            return db.Telefons.Count(e => e.TelefonNummerID == id) > 0;
        }
    }
}