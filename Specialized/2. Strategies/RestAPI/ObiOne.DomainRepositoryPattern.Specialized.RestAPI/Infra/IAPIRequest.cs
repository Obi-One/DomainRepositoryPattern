using System.Collections.Generic;

namespace ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Infra{
    /// <summary>
    /// Interface IAPIRequest
    /// </summary>
    /// <typeparam name="TAPIEntity">The type of the api entity.</typeparam>
    /// <typeparam name="TAPIKey">The type of the api entity key.</typeparam>
    public interface IAPIRequest<TAPIEntity, TAPIKey>
    {
        /// <summary>
        /// GET (and HEAD) is idempotent, which means that making multiple identical requests ends up having the same result as a single request.
        /// That is, they can be called without risk of data modification or corruption—calling it once has the same effect as calling it 10 times, or none at all.
        /// The HTTP GET method is used to **read** (or retrieve) a representation of a resource.
        /// According to the design of the HTTP specification, GET (along with HEAD) requests are used only to read data and not change it.
        /// </summary>
        /// <returns>ICollection&lt;TAPIEntity&gt;.</returns>
        IEnumerable<TAPIEntity> GET();

        /// <summary>
        /// The HTTP GET method is used to **read** (or retrieve) a representation of a resource.
        /// GET (and HEAD) is idempotent, which means that making multiple identical requests ends up having the same result as a single request.
        /// That is, they can be called without risk of data modification or corruption—calling it once has the same effect as calling it 10 times, or none at all.
        /// According to the design of the HTTP specification, GET (along with HEAD) requests are used only to read data and not change it.
        /// </summary>
        /// <param name="aAPIKey">The API entity key.</param>
        /// <returns>The API Entity.</returns>
        TAPIEntity GET(TAPIKey aAPIKey);

        /// <summary>
        /// The POST verb is most-often utilized to **create** new resources.
        /// POST is neither safe nor idempotent. It is therefore recommended for non-idempotent resource requests.
        /// Making two identical POST requests will most-likely result in two resources containing the same information.
        /// In particular, it's used to create subordinate resources.  That is, subordinate to some other (e.g. parent) resource.
        /// In other words, when creating a new resource, POST to the parent and the service takes care of associating the new resource with the parent, assigning an ID (new resource URI), etc.
        /// </summary>
        /// <param name="aAPIEntity">The API entity.</param>
        /// <returns>On successful creation, return HTTP status 201, returning a Location header with a link to the newly-created resource with the 201 HTTP status.</returns>
        TAPIKey POST(TAPIEntity aAPIEntity);

        /// <summary>
        /// PUT is most-often utilized for **update** capabilities, PUT-ing to a known resource URI with the request body containing the newly-updated representation of the original resource.
        /// PUT is not a safe operation, in that it modifies (or creates) state on the server, but it is idempotent.
        /// In other words, if you create or update a resource using PUT and then make that same call again, the resource is still there and still has the same state as it did with the first call.
        /// However, PUT can also be used to create a resource in the case where the resource ID is chosen by the client instead of by the server.
        /// In other words, if the PUT is to a URI that contains the value of a non-existent resource ID.
        /// </summary>
        /// <param name="aAPIEntity">The API entity.</param>
        void PUT(TAPIEntity aAPIEntity);



        /// <summary>
        /// PATCH is used for **modify** capabilities. The PATCH request only needs to contain the changes to the resource, not the complete resource.
        /// PATCH is neither safe nor idempotent.
        /// However, a PATCH request can be issued in such a way as to be idempotent, which also helps prevent bad outcomes from collisions between two PATCH requests on the same resource in a similar time frame.
        /// Collisions from multiple PATCH requests may be more dangerous than PUT collisions because some patch formats need to operate from a known base-point or else they will corrupt the resource. Clients using this kind of patch application should use a conditional request such that the request will fail if the resource has been updated since the client last accessed the resource. For example, the client can use a strong ETag in an If-Match header on the PATCH request.
        /// This resembles PUT, but the body contains a set of instructions describing how a resource currently residing on the server should be modified to produce a new version.
        /// This means that the PATCH body should not just be a modified part of the resource, but in some kind of patch language like JSON Patch or XML Patch.
        /// </summary>
        /// <param name="aAPIEntity">The API entity.</param>
        void PATCH(TAPIEntity aAPIEntity);



        /// <summary>
        /// DELETE is pretty easy to understand. It is used to **delete** a resource identified by a URI.
        /// HTTP-spec-wise, DELETE operations are idempotent. If you DELETE a resource, it's removed.
        /// Repeatedly calling DELETE on that resource ends up the same: the resource is gone.
        /// If calling DELETE say, decrements a counter (within the resource), the DELETE call is no longer idempotent.
        /// As mentioned previously, usage statistics and measurements may be updated while still considering the service idempotent as long as no resource data is changed. Using POST for non-idempotent resource requests is recommended.
        /// There is a caveat about DELETE idempotence, however. Calling DELETE on a resource a second time will often return a 404 (NOT FOUND) since it was already removed and therefore is no longer findable.
        /// This, by some opinions, makes DELETE operations no longer idempotent, however, the end-state of the resource is the same.
        /// </summary>
        /// <param name="aAPIKey">The API key.</param>
        void DELETE(TAPIKey aAPIKey);        
    }
}
