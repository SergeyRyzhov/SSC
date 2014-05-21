using System.Collections.Generic;

using Leaf.Web.Models;

namespace Leaf.Web.Areas.Manager.Models
{
    public class PendingConferenceListModel : ValidModel
    {
        public PendingConferenceListModel()
        {
            this.Conferences = new List<PendingConferenceModel>();
        }

        public List<PendingConferenceModel> Conferences { get; private set; }
    }
}