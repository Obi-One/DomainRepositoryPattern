using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Model;

namespace ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Infra{
    public class APIRequestMapping
    {
        private readonly ConcurrentDictionary<Type, KeyValuePair<Type, object[]>> mAPIRequestMapping = new ConcurrentDictionary<Type, KeyValuePair<Type, object[]>>();

        public APIRequest<TAPIEntity, TAPIKey> CreateInstance<TAPIEntity, TAPIKey>(APIConnectionInfo aAPIConnectionInfo)
            where TAPIEntity : APIEntity<TAPIKey>, new() where TAPIKey : new(){

            var lAPIRequestCtor = mAPIRequestMapping.ContainsKey(typeof(TAPIEntity))
                                               ? mAPIRequestMapping[typeof(TAPIEntity)]
                                               : new KeyValuePair<Type, object[]>(typeof(APIRequest<TAPIEntity, TAPIKey>), null);

            var lAPIConnectionInfo = new object[1];
            lAPIConnectionInfo[0] = aAPIConnectionInfo;
            var lArgs = lAPIConnectionInfo.Concat(lAPIRequestCtor.Value).ToArray();

            return (APIRequest<TAPIEntity, TAPIKey>)Activator.CreateInstance(lAPIRequestCtor.Key, lArgs);
        }

        public void Map<TAPIEntity, TAPIRequest>(params object[] aArgs){
            var lAPIRequestCtor = new KeyValuePair<Type, object[]>(typeof(TAPIRequest), aArgs);

            mAPIRequestMapping.AddOrUpdate(typeof(TAPIEntity), lAPIRequestCtor, (aEntityTypeFind, aRequestPathFind) => lAPIRequestCtor);
        }
    }
}