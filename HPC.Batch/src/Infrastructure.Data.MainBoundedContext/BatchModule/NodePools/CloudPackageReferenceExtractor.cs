
namespace Infrastructure.Data.MainBoundedContext.BatchModule.NodePools
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Applications;
    using Microsoft.Azure.Batch;

    internal class CloudPackageReferenceExtractor
    {
        private readonly CloudPool cloudPool;
        private readonly IEnumerable<PackageReference> packageReferences;

        public CloudPackageReferenceExtractor(CloudPool cloudPool, IEnumerable<PackageReference> packageReferences)
        {
            this.cloudPool = cloudPool;
            this.packageReferences = packageReferences;
        }

        public void Extract()
        {
            cloudPool.ApplicationPackageReferences = new List<ApplicationPackageReference>(packageReferences.Count());

            foreach (var app in packageReferences)
            {
                cloudPool.ApplicationPackageReferences.Add(
                    new ApplicationPackageReference
                    {
                        ApplicationId = app.Identifier,
                        Version = app.Version
                    });
            }
        }
    }
}
