namespace ObiOne.DomainRepositoryPattern.Specialized.EF.Infra{
    public enum EnInitializer{
        SuppressDatabaseInitialization,
        CreateDatabaseIfNotExists,
        DropCreateDatabaseIfModelChanges,
        DropCreateDatabaseAlways
    }
}