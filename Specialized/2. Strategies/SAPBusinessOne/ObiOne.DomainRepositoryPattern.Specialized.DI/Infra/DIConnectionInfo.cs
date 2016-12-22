using System.Collections.Generic;

namespace ObiOne.DomainRepositoryPattern.Specialized.DI.Infra {
    public class DIConnectionInfo {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DIConnectionInfo(string aCompanyDb, string aUserName, string aPassword){
            ServerType = EnServerType.MSSQL2012;
            Server = "localhost";
            UseTrusted = true;
            LicenseServerHost = "localhost";
            LicenseServerPort = 30000;
            Language = EnLanguage.Portuguese_Br;
            CompanyDB = aCompanyDb;
            UserName = aUserName;
            Password = aPassword;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DIConnectionInfo(string aServer, string aCompanyDb, string aUserName, string aPassword, string aLicenseServerHost = null, Dictionary<string, object> aExtra = null)
        {
            ServerType = EnServerType.MSSQL2012;
            Server = aServer;
            UseTrusted = true;
            CompanyDB = aCompanyDb;
            LicenseServerHost = aLicenseServerHost ?? aServer;
            LicenseServerPort = 30000;
            Language = EnLanguage.Portuguese_Br;
            UserName = aUserName;
            Password = aPassword;
            Extra = aExtra ?? new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DIConnectionInfo(string aServer, string aDbUserName, string aDbPassword, string aCompanyDb, string aUserName, string aPassword, string aLicenseServerHost = null, Dictionary<string, object> aExtra = null)
        {
            ServerType = EnServerType.MSSQL2012;
            Server = aServer;
            UseTrusted = false;
            DbUserName = aDbUserName;
            DbPassword = aDbPassword;
            CompanyDB = aCompanyDb;
            LicenseServerHost = aLicenseServerHost ?? aServer;
            LicenseServerPort = 30000;
            Language = EnLanguage.Portuguese_Br;
            UserName = aUserName;
            Password = aPassword;
            Extra = aExtra ?? new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DIConnectionInfo(string aCompanyDb, string aUserName, string aPassword, string aServer, string aDbUserName, string aDbPassword, EnServerType aServerType, string aLicenseServerHost, int aLicenseServerPort, EnLanguage aLanguage){
            ServerType = aServerType;
            Server = aServer;
            UseTrusted = false;
            DbUserName = aDbUserName;
            DbPassword = aDbPassword;
            CompanyDB = aCompanyDb;
            LicenseServerHost = aLicenseServerHost;
            LicenseServerPort = aLicenseServerPort;
            Language = aLanguage;
            UserName = aUserName;
            Password = aPassword;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DIConnectionInfo(string aCompanyDb, string aUserName, string aPassword, string aServer, EnServerType aServerType, string aLicenseServerHost, int aLicenseServerPort, EnLanguage aLanguage){
            ServerType = aServerType;
            Server = aServer;
            UseTrusted = true;
            CompanyDB = aCompanyDb;
            LicenseServerHost = aLicenseServerHost;
            LicenseServerPort = aLicenseServerPort;
            Language = aLanguage;
            UserName = aUserName;
            Password = aPassword;
        }

        public EnServerType ServerType { get; set; }
        public string Server { get; set; }
        public bool UseTrusted { get; set; }
        public string DbUserName { get; set; }
        public string DbPassword { get; set; }
        public string CompanyDB { get; set; }
        public string LicenseServerHost { get; set; }
        public int LicenseServerPort { get; set; }
        public EnLanguage Language { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Dictionary<string, object> Extra { get; set; }
    }
}
