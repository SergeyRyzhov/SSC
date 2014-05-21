using System;
using System.Linq.Expressions;
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
        public ActionResult Create([Bind(Include = "Code,Name,StartDate,EndDate,Description,ShortDescription")] CreateConferenceModel conference)
        {
            if (ModelState.IsValid)
            {
                m_repository.Create(conference);
                return RedirectToAction("Index");
            }

            return View(conference);
        }
    }
}