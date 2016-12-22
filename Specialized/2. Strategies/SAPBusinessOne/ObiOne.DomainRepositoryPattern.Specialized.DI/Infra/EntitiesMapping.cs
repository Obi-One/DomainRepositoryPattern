using System;
using System.Collections.Concurrent;

namespace ObiOne.DomainRepositoryPattern.Specialized.DI.Infra{
    public class EntitiesMapping{
        private readonly ConcurrentDictionary<Type, EnObjectTypes> mEntitiesMapping = new ConcurrentDictionary<Type, EnObjectTypes>();

        public void MapObjectType<TEntity>(EnObjectTypes aBoObjectTypes, Func<TEntity, object> aKeyProperty)
        {
            mEntitiesMapping.AddOrUpdate(typeof(TEntity), aBoObjectTypes, (aEntityTypeFind, aBoObjectTypesFind) => aBoObjectTypes);
        }

        public EnObjectTypes GetObjectType<TEntity>(){
            var lType = typeof(TEntity);

            if (!mEntitiesMapping.ContainsKey(lType)) throw new ApplicationException($"Tipo {lType} não adicionado ao contexto. Adicione no método OnModelCreating pelo MapObjectType<>.");

            var lBoObjectTypes = mEntitiesMapping[lType];
            
            return lBoObjectTypes;
        }

    }
}