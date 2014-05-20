namespace Leaf.DataAccess.Model
{
    public class File : WebEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
    }
}