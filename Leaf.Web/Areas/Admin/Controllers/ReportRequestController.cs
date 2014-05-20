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
    public class ReportRequestController : Controller
    {
        private DataContext db = new DataContext();

        // GET: /Admin/ReportRequest/
        public ActionResult Index()
        {
            return View(db.ReportRequests.ToList());
        }

        // GET: /Admin/ReportRequest/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportRequest reportrequest = db.ReportRequests.Find(id);
            if (reportrequest == null)
            {
                return HttpNotFound();
            }
            return View(reportrequest);
        }

        // GET: /Admin/ReportRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/ReportRequest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,ReportId,Approved")] ReportRequest reportrequest)
        {
            if (ModelState.IsValid)
            {
                reportrequest.Id = Guid.NewGuid();
                db.ReportRequests.Add(reportrequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reportrequest);
        }

        // GET: /Admin/ReportRequest/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportRequest reportrequest = db.ReportRequests.Find(id);
            if (reportrequest == null)
            {
                return HttpNotFound();
            }
            return View(reportrequest);
        }

        // POST: /Admin/ReportRequest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ReportId,Approved")] ReportRequest reportrequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportrequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reportrequest);
        }

        // GET: /Admin/ReportRequest/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportRequest reportrequest = db.ReportRequests.Find(id);
            if (reportrequest == null)
            {
                return HttpNotFound();
            }
            return View(reportrequest);
        }

        // POST: /Admin/ReportRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ReportRequest reportrequest = db.ReportRequests.Find(id);
            db.ReportRequests.Remove(reportrequest);
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
