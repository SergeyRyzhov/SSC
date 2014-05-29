using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

using Leaf.DataAccess.Contexts;
using Leaf.DataAccess.Model;
using Leaf.Web.Helpers;
using Leaf.Web.Repositories;

namespace Leaf.Web.Areas.Content.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportRepository m_repository;

        public ReportController()
        {
            this.m_repository = new ReportRepository();
        }

        public ActionResult Index(string id)
        {
            var model = m_repository.GetReports(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create(string id)
        {
            return this.View(new ReportViewModel() { ConfId = id, Owner = ClientContext.Current.User.Identity.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConfId,Name,Owner,ShortDescription,Description")] ReportViewModel report)
        {
            if (ModelState.IsValid)
            {
                m_repository.Create(report);
                return RedirectToAction("Index", new { id = report.ConfId });
            }

            return View(report);
        }
    }

    public class ReportRepository : IReportRepository
    {
        private IConferenceRepository m_repository;
        private readonly DataContext m_context;

        public ReportRepository()
        {
            m_repository = new ConferenceRepository();
            m_context = new DataContext();
        }

        public ReportListModel GetReports(string confId)
        {
            var conf = m_context.Reports.Where(c => c.ConferenceCode.Equals(confId, StringComparison.InvariantCultureIgnoreCase)).ToArray();
            var model = new ReportListModel { ConferenceId = confId };

            model.Reports.AddRange(conf.Select(r => new ReportModel(r)));

            return model;
        }

        public void Create(ReportViewModel model)
        {
            var report = new Report
                             {
                                 Id = Guid.NewGuid(),
                                 ConferenceCode = model.ConfId,
                                 Owner = model.Owner,
                                 Name = model.Name,
                                 Description = model.Description,
                                 ShortDescription = model.ShortDescription,
                                 IsPublished = false,
                                 PublishDate = SqlDateTime.MinValue.Value,
                                 Files = new List<File>(),
                                 Images = new List<Image>()
                             };
            m_context.Reports.Add(report);
            m_context.SaveChanges();
            /*
            var saved = m_context.Conferences.Where(ec => ec.Code.Equals(model.ConfId)).ToArray()[0];
            saved.Reports = saved.Reports ?? new List<Report>();
            saved.Reports.Add(report);
            m_context.Entry(saved).State = EntityState.Modified;
            m_context.SaveChanges();*/

            /*
            var saved = m_context.Reports.Where(ec => ec.Name.Equals(report.Name)).ToArray()[0];
            var request = new ReportRequest { Id = Guid.NewGuid(), ConferenceId = saved.Id, Approved = false };
            m_context.ConferenceRequests.Add(request);
            m_context.SaveChanges();*/
        }

        private Conference GetConferenceById(string id)
        {
            try
            {
                return this.m_context.Conferences.Where(c => c.Code.Equals(id, StringComparison.InvariantCultureIgnoreCase)).ToArray()[0];
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return null;
        }
    }

    public interface IReportRepository
    {
        ReportListModel GetReports(string confId);

        void Create(ReportViewModel report);
    }

    public class ReportListModel
    {
        public ReportListModel()
        {
            this.Reports = new List<ReportModel>();
        }

        public List<ReportModel> Reports { get; private set; }

        public string ConferenceId { get; set; }
    }

    public class ReportModel
    {
        private readonly Report m_report;

        public ReportModel(Report report)
        {
            m_report = report;
        }


        public string Owner
        {
            get
            {
                return m_report.Owner;
            }
        }

        public string Name
        {
            get
            {
                return m_report.Name;
            }
        }

        public string ShortDescription
        {
            get
            {
                return m_report.ShortDescription;
            }
        }

        public string Description
        {
            get
            {
                return m_report.Description;
            }
        }
    }

    public class ReportViewModel
    {
        public string ConfId { get; set; }

        public string Owner { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }
    }
}