using System;

namespace Leaf.DataAccess.Model
{
    public abstract class WebEntity : Entity
    {
        public bool IsPublished { get; set; }

        public DateTime PublishDate { get; set; }
    }
}