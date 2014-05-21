using System;
using System.Data.Entity;
using System.Linq;

using Leaf.DataAccess.Contexts;
using Leaf.DataAccess.Model;
using Leaf.Web.Areas.Content.Models;
using Leaf.Web.Areas.Manager.Models;

namespace Leaf.Web.Repositories
{
    public interface IConferenceRequestRepository
    {
        PendingConferenceListModel GetPendingConferenceList();

        void Approve(string code);
    }

    public class ConferenceRequestRepository : IConferenceRequestRepository
    {
        private readonly DataContext m_context;

        public ConferenceRequestRepository()
        {
            this.m_context = new DataContext();
        }

        public PendingConferenceListModel GetPendingConferenceList()
        {
            var model = new PendingConferenceListModel { IsValid = true };
            try
            {
                var conf  = m_context.Conferences.Where(c => !c.IsPublished).ToArray();
                
                var req = m_context.ConferenceRequests.Where(r => !r.Approved).ToArray();

                foreach (ConferenceRequest request in req)
                {
                    Conference conference = conf.FirstOrDefault(c => c.Id.Equals(request.ConferenceId));

                    if (conference != null)
                    {
                        model.Conferences.Add(new PendingConferenceModel(conference, request));
                    }
                }
            }
            catch (Exception exception)
            {
                model.IsValid = false;
                model.Message = exception.ToString();
            }

            return model;
        }

        public void Approve(string code)
        {
            try
            {
                var conf = m_context.Conferences.Where(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase)).ToArray()[0];

                var req = m_context.ConferenceRequests.Where(r => r.ConferenceId.Equals(conf.Id)).ToArray()[0];
                conf.IsPublished = true;
                conf.PublishDate = DateTime.Now;


                m_context.Entry(conf).State = EntityState.Modified;
                m_context.SaveChanges();

                req.Approved = true;
                m_context.Entry(req).State = EntityState.Modified;
                m_context.SaveChanges();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}