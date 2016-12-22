using ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Infra;

namespace ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Tests
{
    public class TestAPIConnectionList{
        public APIConnectionInfo TestAPIConnection => mTestAPIAtJSONPlaceholder;

        private readonly APIConnectionInfo mTestAPIAtJSONPlaceholder = new APIConnectionInfo("http://jsonplaceholder.typicode.com/");
    }
}
