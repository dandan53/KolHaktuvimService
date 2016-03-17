using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KolHaktuvimService.Models;

namespace KolHaktuvimService.Controllers
{   
    public class PeopleController : Controller
    {
        private KolHaktuvimServiceContext context = new KolHaktuvimServiceContext();

        //
        // GET: /People/

        public ViewResult Index()
        {
            return View(context.People.ToList());
        }

        //
        // GET: /People/Details/5

        public ViewResult Details(int id)
        {
            Person person = context.People.Single(x => x.PersonId == id);
            return View(person);
        }

        //
        // GET: /People/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /People/Create

        [HttpPost]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                context.People.Add(person);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(person);
        }
        
        //
        // GET: /People/Edit/5
 
        public ActionResult Edit(int id)
        {
            Person person = context.People.Single(x => x.PersonId == id);
            return View(person);
        }

        //
        // POST: /People/Edit/5

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                context.Entry(person).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        //
        // GET: /People/Delete/5
 
        public ActionResult Delete(int id)
        {
            Person person = context.People.Single(x => x.PersonId == id);
            return View(person);
        }

        //
        // POST: /People/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = context.People.Single(x => x.PersonId == id);
            context.People.Remove(person);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}