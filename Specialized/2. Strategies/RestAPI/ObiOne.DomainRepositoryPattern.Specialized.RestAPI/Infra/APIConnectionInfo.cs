namespace ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Infra{
    public class APIConnectionInfo{
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public APIConnectionInfo(string aEndpoint){
            Endpoint = aEndpoint;
        }

        public string Endpoint { get; set; }
    }
}