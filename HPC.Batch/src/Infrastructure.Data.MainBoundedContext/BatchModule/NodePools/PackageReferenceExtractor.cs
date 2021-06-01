
namespace Infrastructure.Data.MainBoundedContext.BatchModule.NodePools
{
    using System.Collections.Generic;
    using Microsoft.Azure.Batch;

    internal class PackageReferenceExtractor
    {
        private readonly CloudPool cloudPool;

        public PackageReferenceExtractor(CloudPool cloudPool)
        {
            this.cloudPool = cloudPool;
        }

        public IDictionary<string,string> Extract()
        {
            var output = new Dictionary<string, string>();
            foreach (var resource in this.cloudPool.ApplicationPackageReferences)
            {
                /*if (output.ContainsKey(resource.ApplicationId)
                    && output.TryGetValue(resource.ApplicationId, out string version) && )
                {

                }*/
                output.Add(resource.ApplicationId, resource.Version);
            }
            return output;
        }
    }
}
