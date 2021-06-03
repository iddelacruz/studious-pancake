namespace Domain.MainBoundedContext.BatchModule.Aggregates.Resources
{
    using System;

    public sealed class FromAutoStorageResourceFile
    {
        /// <summary>
        /// Get the storage container name in the auto storage account.
        /// </summary>
        public string AutoStorageContainerName { get; private set; }


        /// <summary>
        /// Gets the blob name to download.
        /// </summary>
        public string BlobName { get; private set; }

        /// <summary>
        /// Create a new instance of <see cref="FromUriResourceFile"/>
        /// </summary>
        /// <param name="autoStorageContainerName">The storage container name in the auto storage account.</param>
        /// <param name="blobName">The blob name to use when downloading blobs from a storage container.</param>
        internal FromAutoStorageResourceFile(string autoStorageContainerName, string blobName)
        {
            if (string.IsNullOrWhiteSpace(autoStorageContainerName))
            {
                throw new ArgumentNullException(nameof(autoStorageContainerName));
            }
            if (string.IsNullOrWhiteSpace(blobName))
            {
                throw new ArgumentNullException(nameof(blobName));
            }
            this.AutoStorageContainerName = autoStorageContainerName;
            this.BlobName = blobName;
        }
    }
}
