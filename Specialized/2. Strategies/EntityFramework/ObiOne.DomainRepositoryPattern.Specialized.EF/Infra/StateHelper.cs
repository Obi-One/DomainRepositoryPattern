using System;
using System.Data.Entity;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Infra;

namespace ObiOne.DomainRepositoryPattern.Specialized.EF.Infra {
    public class StateHelper
    {
        public static EntityState ConvertState(EnObjectState state)
        {
            switch (state)
            {
                case EnObjectState.Added:
                    return EntityState.Added;

                case EnObjectState.Modified:
                    return EntityState.Modified;

                case EnObjectState.Deleted:
                    return EntityState.Deleted;

                default:
                    return EntityState.Unchanged;
            }
        }

        public static EnObjectState ConvertState(EntityState state)
        {
            switch (state)
            {
                case EntityState.Detached:
                    return EnObjectState.Unchanged;

                case EntityState.Unchanged:
                    return EnObjectState.Unchanged;

                case EntityState.Added:
                    return EnObjectState.Added;

                case EntityState.Deleted:
                    return EnObjectState.Deleted;

                case EntityState.Modified:
                    return EnObjectState.Modified;

                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }
    }
}
