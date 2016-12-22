using ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Model;

namespace ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Tests.Model
{
    public class Posts : APIEntity<int>
    {
        public int userId { get; set; }

        public int id { get; set; }

        public string title { get; set; }

        public string body { get; set; }
    }
}
