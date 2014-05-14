using System;

namespace Leaf.DataAccess.Model
{
    public abstract class WebEntity : Entity
    {
        public bool Published { get; set; }

        public DateTime PublishDate { get; set; }
    }
}