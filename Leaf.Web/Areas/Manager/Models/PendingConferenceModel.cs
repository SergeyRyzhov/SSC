using System.Globalization;

using Leaf.DataAccess.Model;

namespace Leaf.Web.Areas.Manager.Models
{
    public class PendingConferenceModel
    {
        private readonly Conference m_conference;
        private readonly ConferenceRequest m_request;

        public PendingConferenceModel(Conference conference, ConferenceRequest request)
        {
            this.m_conference = conference;
            this.m_request = request;
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

        public string Description
        {
            get
            {
                return this.m_conference.ShortDescription;
            }
        }

        public string SartDate
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