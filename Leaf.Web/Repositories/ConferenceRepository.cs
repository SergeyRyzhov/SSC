using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

using Leaf.DataAccess.Contexts;
using Leaf.DataAccess.Model;
using Leaf.Web.Areas.Content.Models;

using WebGrease.Css.Extensions;

namespace Leaf.Web.Repositories
{
    public interface IConferenceRepository
    {
        ConferenceListModel GetPublishedConferenceList();

        void Create(CreateConferenceModel model);
    }

    public class ConferenceRepository : IConferenceRepository
    {
        private readonly DataContext m_context;

        public ConferenceRepository()
        {
            this.m_context = new DataContext();
        }

        public ConferenceListModel GetPublishedConferenceList()
        {
            var model = new ConferenceListModel { IsValid = true };
            try
            {
                m_context.Conferences.Where(c => c.IsPublished)
                    .ForEach(c => model.Conferences.Add(new Areas.Content.Models.ConferenceModel(c)));
            }
            catch (Exception exception)
            {
                model.IsValid = false;
                model.Message = exception.ToString();
            }

            return model;
        }

        public void Create(CreateConferenceModel model)
        {
            var conference = new Conference
                        {
                            Id = Guid.NewGuid(),
                            Code = model.Code,
                            Name = model.Name,
                            Description = model.Description,
                            ShortDescription = model.ShortDescription,
                            StartDate = model.StartDate,
                            EndDate = model.EndDate,
                            IsPublished = false,
                            PublishDate = SqlDateTime.MinValue.Value,
                            Files = new List<File>(),
                            Images = new List<Image>(),
                            Reports = new List<Report>()
                        };

            m_context.Conferences.Add(conference);
            m_context.SaveChanges();

            var saved = m_context.Conferences.Where(ec => ec.Code.Equals(conference.Code)).ToArray()[0];
            var request = new ConferenceRequest { Id = Guid.NewGuid(), ConferenceId = saved.Id, Approved = false };
            m_context.ConferenceRequests.Add(request);
            m_context.SaveChanges();
        }
    }
}