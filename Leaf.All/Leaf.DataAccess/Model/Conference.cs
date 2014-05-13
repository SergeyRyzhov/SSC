using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.DataAccess.Model
{
    public abstract class Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }

    public abstract class WebEntity : Entity
    {
        public bool Published { get; set; }

        public DateTime PublishDate { get; set; }
    }

    public class Conference : WebEntity
    {
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public List<Image> Images { get; set; }

        public List<File> Files { get; set; }

        public List<Report> Reports { get; set; }
    }

    public class File : WebEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
    }

    public class Image : WebEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
    }

    public class Report : WebEntity
    {
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public List<Image> Images { get; set; }

        public List<File> Files { get; set; }

    }

    public abstract class Request : Entity
    {
        public bool Approved { get; set; }
    }

    public class ConferenceRequest : Request
    {
        public Guid ConferenceId { get; set; }
    }

    public class ReportRequest : Request
    {
        public Guid ReportId { get; set; }
    }
}
