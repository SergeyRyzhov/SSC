using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Leaf.DataAccess.Model;
using Leaf.DataAccess.Contexts;

namespace Leaf.Web.Areas.Admin.Controllers
{
    public class ConferenceRequestController : Controller
    {
        private DataContext db = new DataContext();

        // GET: /Admin/ConferenceRequest/
        public ActionResult Index()
        {
            return View(db.ConferenceRequests.ToList());
        }

        // GET: /Admin/ConferenceRequest/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConferenceRequest conferencerequest = db.ConferenceRequests.Find(id);
            if (conferencerequest == null)
            {
                return HttpNotFound();
            }
            return View(conferencerequest);
        }

        // GET: /Admin/ConferenceRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/ConferenceRequest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,ConferenceId,Approved")] ConferenceRequest conferencerequest)
        {
            if (ModelState.IsValid)
            {
                conferencerequest.Id = Guid.NewGuid();
                db.ConferenceRequests.Add(conferencerequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(conferencerequest);
        }

        // GET: /Admin/ConferenceRequest/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConferenceRequest conferencerequest = db.ConferenceRequests.Find(id);
            if (conferencerequest == null)
            {
                return HttpNotFound();
            }
            return View(conferencerequest);
        }

        // POST: /Admin/ConferenceRequest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ConferenceId,Approved")] ConferenceRequest conferencerequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conferencerequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(conferencerequest);
        }

        // GET: /Admin/ConferenceRequest/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConferenceRequest conferencerequest = db.ConferenceRequests.Find(id);
            if (conferencerequest == null)
            {
                return HttpNotFound();
            }
            return View(conferencerequest);
        }

        // POST: /Admin/ConferenceRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ConferenceRequest conferencerequest = db.ConferenceRequests.Find(id);
            db.ConferenceRequests.Remove(conferencerequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
