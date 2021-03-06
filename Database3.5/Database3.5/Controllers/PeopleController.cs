﻿using System;
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
    public class PeopleController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/People
        public IQueryable<PersonDTO> GetPeople()
        {
            var people = from a in db.People
                            select new PersonDTO()
                            {
                                Name = a.Fornavn + " " + a.Mellemnavn + " " + a.Efternavn,
                                PersonNummer = a.PersonNummer,
                                Adresse = a.Adresse.Vejnavn + " " + a.Adresse.Husnummer + ", " + a.Adresse.Postnummer_ + " " + a.Adresse.Postnummer_
                            };
            return people;
        }

        // GET: api/People/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult GetPerson(long id)
        {
           
            var person = db.People.Include(b => b.Adresse).Select(b =>
            new PersonDTO()
            {            
                PersonNummer = b.PersonNummer,
                Adresse = b.Adresse.Vejnavn + " " + b.Adresse.Husnummer + ", " + b.Adresse.Postnummer_ + " " + b.Adresse.Postnummer_,
                Name = b.Fornavn+ b.Mellemnavn+ b.Efternavn
                
            }).SingleOrDefaultAsync(b => b.PersonNummer == id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/People/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerson(long id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.PersonNummer)
            {
                return BadRequest();
            }

            db.Entry(person).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/People
        [ResponseType(typeof(Person))]
        public IHttpActionResult PostPerson(PersonPostDTO persondto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = new Person
            {
                AdresseID = persondto.AdresseId,
                Adresse = null,
                Efternavn = persondto.Efternavn,
                Fornavn = persondto.Fornavn,
                Type = persondto.Type,
                PersonNummer = persondto.Personnummer,
                erPaas = null,
                hars = null
            };


            db.People.Add(person);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PersonExists(person.PersonNummer))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            db.Entry(person).Reference(x=>x.Adresse).Load();


            return CreatedAtRoute("DefaultApi", new { id = person.PersonNummer }, persondto);
        }

        // DELETE: api/People/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult DeletePerson(long id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            db.People.Remove(person);
            db.SaveChanges();

            return Ok(person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(long id)
        {
            return db.People.Count(e => e.PersonNummer == id) > 0;
        }
    }
}