namespace ObiOne.DomainRepositoryPattern.Specialized.EF.Infra
{
    public class EFConnectionInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public EFConnectionInfo(string aServer, string aCatalog, EnInitializer aInitializer){
            Catalog = aCatalog;
            Initializer = aInitializer;
            Server = aServer;
            Trusted = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public EFConnectionInfo(string aServer, string aUsername, string aPassword, string aCatalog, EnInitializer aInitializer)
        {
            Catalog = aCatalog;
            Initializer = aInitializer;
            Password = aPassword;
            Server = aServer;
            Username = aUsername;
            Trusted = false;
        }

        public string Server { get; set; }
        public string Catalog { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Trusted { get; set; }

        public string Conn => Trusted
                                  ? $"Type System Version=SQL Server 2012;Data Source={Server};Persist Security Info=true;Initial Catalog={Catalog};Trusted_Connection=True;"
                                  : $"Type System Version=SQL Server 2012;Data Source={Server};Persist Security Info=true;Initial Catalog={Catalog};User ID={Username};Password={Password};";

        public EnInitializer Initializer { get; set; }
    }
}
