﻿using System.Collections.Generic;

namespace Leaf.DataAccess.Model
{
    public class Report : WebEntity
    {
        public string Owner { get; set; }

        public string ConferenceCode { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public List<Image> Images { get; set; }

        public List<File> Files { get; set; }
    }
}