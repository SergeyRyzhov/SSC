using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leaf.Web.Areas.Content.Models
{
    public class CreateConferenceModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }
    }
}