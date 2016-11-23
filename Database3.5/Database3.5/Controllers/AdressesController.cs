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
        public List<AdresseDTO> GetAdresses()
        {
            var addresses = from a in db.Adresses.Include("erPaas.Person") select a;

            List<AdresseDTO> dtolist = new List<AdresseDTO>();

            foreach (var a in addresses)
            {
                var dto = new AdresseDTO
                {
                    AdresseID = a.AdresseID,
                    Postnummer_ = a.Postnummer_,
                    People = new List<PersonRefAdresse>()
                    
                };
                //ExceptionMessage": "Self referencing loop detected with type 'System.Data.Entity.DynamicProxies.erPaa_BAEA9B1FF9103E5EB84AD41940B3F06CAE9CCA85358921F3AE00F3041BC95437'. Path '[1].People[0].erPaas[0].Adresse.erPaas'.",
                foreach (var p in a.erPaas)
                {
                    var person = new PersonRefAdresse
                    {
                        Navn = p.Person.Fornavn,
                        Personnummer = p.Person.PersonNummer.ToString()
                    };
                    dto.People.Add(person);
                }

                dtolist.Add(dto);
            }


            return dtolist;
        }

        // GET: api/Adresses/5
        [ResponseType(typeof(Adresse))]
        public IHttpActionResult GetAdresse(long id)
        {
            var adresse = from a in db.Adresses.Include("erPaas.Person") where a.AdresseID == id select a;
            var adresse2 = adresse.FirstOrDefault();
            var dto = new AdresseDTO()
            {
                AdresseID = adresse2.AdresseID,
                Postnummer_ = adresse2.Postnummer_,
                People = new List<PersonRefAdresse>()
            };

            foreach (var p in adresse2.erPaas)
            {
                var person = new PersonRefAdresse
                {
                    Navn = p.Person.Fornavn,
                    Personnummer = p.Person.PersonNummer.ToString()
                };
                dto.People.Add(person);
            }
            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
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
                People = new List<PersonRefAdresse>()
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