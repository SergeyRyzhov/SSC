using System;
using System.Collections.Generic;

namespace Leaf.DataAccess.Model
{
    public class Conference : WebEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public List<Image> Images { get; set; }

        public List<File> Files { get; set; }

        public List<Report> Reports { get; set; }
    }
}