using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;

using Leaf.DataAccess.Contexts;
using Leaf.DataAccess.Model;
using Leaf.Web.Areas.Content.Models;
using Leaf.Web.Helpers;

using WebGrease.Css.Extensions;

namespace Leaf.Web.Repositories
{
    public interface IConferenceRepository
    {
        ConferenceListModel GetPublishedConferenceList();

        ConferenceViewModel GetById(string id);

        void Create(CreateConferenceModel model);

        void Save(ConferenceViewModel model);
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

        public ConferenceViewModel GetById(string id)
        {
            var model = this.GetConferenceById(id);
            var conf = new ConferenceViewModel
                           {
                               Name = model.Name,
                               Description = model.Description,
                               ShortDescription = model.ShortDescription,
                               StartDate = model.StartDate.ToString(CultureInfo.InvariantCulture),
                               EndDate = model.EndDate.ToString(CultureInfo.InvariantCulture)
                           };

            return conf;
        }

        public void Create(CreateConferenceModel model)
        {
            IClientContext clientContext = ClientContext.Current;

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
                            Owner = clientContext.User.Identity.Name,
                            Files = new List<File>(),
                            Images = new List<Image>()/*,
                            Reports = new List<Report>()*/
                        };

            m_context.Conferences.Add(conference);
            m_context.SaveChanges();

            var saved = m_context.Conferences.Where(ec => ec.Code.Equals(conference.Code)).ToArray()[0];
            var request = new ConferenceRequest { Id = Guid.NewGuid(), ConferenceId = saved.Id, Approved = false };
            m_context.ConferenceRequests.Add(request);
            m_context.SaveChanges();
        }

        public void Save(ConferenceViewModel model)
        {
            try
            {
                var conf = GetConferenceById(model.Id);

                conf.Name = model.Name;
                conf.Description = model.Description;
                conf.ShortDescription = model.ShortDescription;
                conf.StartDate = DateTime.Parse(model.StartDate);
                conf.EndDate = DateTime.Parse(model.EndDate);

                m_context.Entry(conf).State = EntityState.Modified;
                m_context.SaveChanges();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
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
}