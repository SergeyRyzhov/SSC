using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Leaf.Web.Repositories;

namespace Leaf.Web.Areas.Manager.Controllers
{
    public class RequestController : Controller
    {
        private readonly IConferenceRequestRepository m_repository;

        public RequestController()
        {
            this.m_repository = new ConferenceRequestRepository();
        }

        public ActionResult Index()
        {
            var model = m_repository.GetPendingConferenceList();
            return View(model);
        }


        [HttpGet]
        public ActionResult Approve(string id)
        {
            var model = m_repository.GetPendingConferenceList();
            var a = model.Conferences.FirstOrDefault(m => m.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
            if (a != null)
            {
                m_repository.Approve(id);
            }

            return RedirectToAction("Index");
        }
    }
}