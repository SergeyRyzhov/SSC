using System;
using System.Linq.Expressions;
using System.Net;
using System.Web.Mvc;

using Leaf.DataAccess.Model;
using Leaf.Web.Areas.Content.Models;
using Leaf.Web.Models;
using Leaf.Web.Repositories;

namespace Leaf.Web.Areas.Content.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly IConferenceRepository m_repository;

        public ConferenceController()
        {
            this.m_repository = new ConferenceRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ConferenceListModel model = this.m_repository.GetPublishedConferenceList();

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code,Name,StartDate,EndDate,ShortDescription,Description")] CreateConferenceModel conference)
        {
            if (ModelState.IsValid)
            {
                m_repository.Create(conference);
                return RedirectToAction("Index");
            }

            return View(conference);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ConferenceViewModel conference = m_repository.GetById(id);
            if (conference == null)
            {
                return HttpNotFound();
            }

            return View(conference);
        }

        // POST: /Admin/Conference/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,StartDate,EndDate,ShortDescription,Description")] ConferenceViewModel conference)
        {
            if (ModelState.IsValid)
            {
                m_repository.Save(conference);
                return RedirectToAction("Index");
            }

            return View(conference);
        }
    }
}