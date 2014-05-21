using System.Collections.Generic;

using Leaf.Web.Models;

namespace Leaf.Web.Areas.Content.Models
{
    public class ConferenceListModel: ValidModel
    {
        public ConferenceListModel()
        {
            this.Conferences = new List<ConferenceModel>();
        }

        public List<ConferenceModel> Conferences { get; private set; }
    }
}