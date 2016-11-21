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
using Database3._5.Models;

namespace Database3._5.Controllers
{
    public class AdressesController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/Adresses
        public IQueryable<AdresseDTO> GetAdresses()
        {
            var addresses = from a in db.Adresses
                select new AdresseDTO()
                {
                    AdresseID=a.AdresseID,
                    Postnummer_ = a.Postnummer_,
                    People = a.People
                };
            return addresses;
        }

        // GET: api/Adresses/5
        [ResponseType(typeof(Adresse))]
        public IHttpActionResult GetAdresse(long id)
        {
            var adresse = db.Adresses.Include(b=> b.People).Select(b=>
            new AdresseDTO()
            {
                AdresseID = b.AdresseID,
                Postnummer_ = b.Postnummer_,
                    People = b.People
            }).SingleOrDefault(b=>b.AdresseID==id);
            
            if (adresse == null)
            {
                return NotFound();
            }

            return Ok(adresse);
        }

        // PUT: api/Adresses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdresse(long id, Adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adresse.AdresseID)
            {
                return BadRequest();
            }

            db.Entry(adresse).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdresseExists(id))
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

        // POST: api/Adresses
        [ResponseType(typeof(Adresse))]
        public IHttpActionResult PostAdresse(Adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Adresses.Add(adresse);
            db.SaveChanges();


            db.Entry(adresse).Reference(x=>x.People).Load();

            var dto = new AdresseDTO()
            {
                AdresseID = adresse.AdresseID,
                Postnummer_ = adresse.Postnummer_,
                People = adresse.People
            };
           
            return CreatedAtRoute("DefaultApi", new { id = adresse.AdresseID }, dto);
        }

        // DELETE: api/Adresses/5
        [ResponseType(typeof(Adresse))]
        public IHttpActionResult DeleteAdresse(long id)
        {
            Adresse adresse = db.Adresses.Find(id);
            if (adresse == null)
            {
                return NotFound();
            }

            db.Adresses.Remove(adresse);
            db.SaveChanges();

            return Ok(adresse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdresseExists(long id)
        {
            return db.Adresses.Count(e => e.AdresseID == id) > 0;
        }
    }
}