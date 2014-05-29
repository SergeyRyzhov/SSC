using System.Globalization;

using Leaf.DataAccess.Model;

namespace Leaf.Web.Areas.Content.Models
{
    public class ConferenceModel
    {
        private readonly Conference m_conference;

        public ConferenceModel(Conference conference)
        {
            this.m_conference = conference;
        }

        public string Id
        {
            get
            {
                return this.m_conference.Code;
            }
        } 
        
        public string Name
        {
            get
            {
                return this.m_conference.Name;
            }
        }

        public string ShortDescription
        {
            get
            {
                return this.m_conference.ShortDescription;
            }
        }

        public string Description
        {
            get
            {
                return this.m_conference.Description;
            }
        }

        public string StartDate
        {
            get
            {
                return this.m_conference.StartDate.ToString(CultureInfo.InvariantCulture);
            }
        }
        
        public string EndDate
        {
            get
            {
                return this.m_conference.EndDate.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}