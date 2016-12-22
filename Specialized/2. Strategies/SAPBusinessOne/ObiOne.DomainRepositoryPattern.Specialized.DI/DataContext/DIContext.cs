using System;
using System.Net.NetworkInformation;
using ObiOne.DomainRepositoryPattern.Specialized.DI.Infra;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.DataContext;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Infra;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Model;
using SAPbobsCOM;

namespace ObiOne.DomainRepositoryPattern.Specialized.DI.DataContext{
    public abstract class DIContext : IDataContext
    {
        protected internal Company SboCompany;
        public EntitiesMapping EntitiesMapping { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        protected DIContext(DIConnectionInfo aDIConnectionInfo){
            SboCompany = new Company{// Common Database
                                        DbServerType = (BoDataServerTypes) aDIConnectionInfo.ServerType
                                        , Server = aDIConnectionInfo.Server
                                        , UseTrusted = aDIConnectionInfo.UseTrusted
                                        , DbUserName = aDIConnectionInfo.DbUserName
                                        , DbPassword = aDIConnectionInfo.DbPassword
                                        // Company Database
                                        , CompanyDB = aDIConnectionInfo.CompanyDB
                                        // License Server
                                        , LicenseServer = aDIConnectionInfo.LicenseServerHost + ":" + aDIConnectionInfo.LicenseServerPort
                                        , language = (BoSuppLangs) aDIConnectionInfo.Language
                                        // Company User
                                        , UserName = aDIConnectionInfo.UserName
                                        // User In Company
                                        , Password = aDIConnectionInfo.Password};

            //if (!PingHost(aDIConnectionInfo.Server)) throw new NetworkInformationException();

            //if (!PingHost(aDIConnectionInfo.LicenseServerHost)) throw new NetworkInformationException();

            this.AsException(SboCompany.Connect());

            EntitiesMapping = new EntitiesMapping();

            // ReSharper disable once VirtualMemberCallInContructor
            OnModelCreating(EntitiesMapping);

        }

        protected abstract void OnModelCreating(EntitiesMapping aEntitiesMapping);
        
        //public DIRepository<TDIEntity, TDIKey> GetDIRepository<TDIEntity, TDIKey>()
        //    where TDIEntity : DIEntity<TDIKey>{
        //    return new DIRepository<TDIEntity, TDIKey>(this);
        //}

        public dynamic Set<TDIEntity>()
        {
            var lObjectType = EntitiesMapping.GetObjectType<TDIEntity>();
            if (lObjectType == EnObjectTypes.oNotExposed) throw new ApplicationException("Tipo não exposto pela DI-API");

            var lBoObjectType = (BoObjectTypes)lObjectType;
            return SboCompany.GetBusinessObject(lBoObjectType);
        }

        #region IDisposable
        private bool mDisposed;

        protected virtual void Dispose(bool aDisposing)
        {
            if (!mDisposed)
            {
                if (aDisposing)
                {
                    if (SboCompany != null)
                    {
                        if (SboCompany.Connected)
                            SboCompany.Disconnect();

                        System.Runtime.InteropServices.Marshal.ReleaseComObject(SboCompany);
                        SboCompany = null;
                    }
                }
            }
            mDisposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } 
        #endregion

        public int SaveChanges(){
            throw new NotSupportedException();
        }

        public void SyncObjectState<TEntity, TKey>(TEntity aEntity) where TEntity : class, IEntity<TKey>, IObjectState{
            throw new NotSupportedException();
        }

        public void SyncObjectsStatePostCommit(){
            throw new NotSupportedException();
        }

        public void AddUserField(string aTableName, string aName, string aDescription, BoFieldTypes aType, BoFldSubTypes aSubType, int aSize)
        {
            if (!GetFieldID(aTableName, aName).HasValue)
            {
                var lUserFieldsMD = (UserFieldsMD)SboCompany.GetBusinessObject(BoObjectTypes.oUserFields);

                lUserFieldsMD.TableName = aTableName;
                lUserFieldsMD.Name = aName;
                lUserFieldsMD.Description = aDescription;
                lUserFieldsMD.Type = aType;
                lUserFieldsMD.SubType = aSubType;
                lUserFieldsMD.EditSize = aSize;
                lUserFieldsMD.Mandatory = BoYesNoEnum.tNO;

                this.AsException(lUserFieldsMD.Add());

                System.Runtime.InteropServices.Marshal.ReleaseComObject(lUserFieldsMD);
                // ReSharper disable once RedundantAssignment
                lUserFieldsMD = null;
                GC.Collect();
            }
        }

        public int? GetFieldID(string aTableName, string aName){
            int? lFieldID = null;
            var lRecordSet = (Recordset)SboCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            lRecordSet.DoQuery($"SELECT FieldID FROM CUFD WHERE TableID = '{aTableName}' AND AliasID = '{aName}'");

            if (lRecordSet.RecordCount == 1){
                lFieldID = lRecordSet.Fields.Item("FieldID")
                                     .Value;
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(lRecordSet);
            // ReSharper disable once RedundantAssignment
            lRecordSet = null;
            GC.Collect();

            return lFieldID;
        }

        public static bool PingHost(string aNameOrAddress)
        {
            var lPingable = false;
            var lPinger = new Ping();
            try{
                var lReply = lPinger.Send(aNameOrAddress);
                if (lReply != null){
                    lPingable = lReply.Status == IPStatus.Success;
                }
            } catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            return lPingable;
        }

    }
}