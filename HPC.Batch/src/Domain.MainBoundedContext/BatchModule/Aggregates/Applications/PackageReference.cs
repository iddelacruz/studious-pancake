namespace Domain.MainBoundedContext.BatchModule.Aggregates.Applications
{
    using System;

    /// <summary>
    /// Application to be used in nodes.
    /// </summary>
    /// <remarks>
    /// You can't use application packages with Azure Storage accounts configured with firewall rules,
    /// or with Hierarchical namespace set to Enabled.
    /// Changes to application package references affect all new compute nodes joining the pool,
    /// but do not affect compute nodes that are already in the pool until they are rebooted or reimaged.
    /// There is a maximum of 10 application package references on any given pool.
    /// </remarks>
    public class PackageReference
    {
        /// <summary>
        /// Get the application unique identifier
        /// </summary>
        /// <remarks>
        /// The Application ID and Version you enter must follow these requirements:
        /// > On Windows nodes, the ID can contain any combination of alphanumeric characters, hyphens, and underscores.
        ///     On Linux nodes, only alphanumeric characters and underscores are permitted.
        /// > Can't contain more than 64 characters.
        /// > Must be unique within the Batch account.
        /// > IDs are case-preserving and case-insensitive.
        /// </remarks>
        public string Identifier { get; private set; }

        /// <summary>
        /// Get the number of versions associated with this application.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Create a new instance of <see cref="PackageReference"/>
        /// </summary>
        /// <param name="identifier"><see cref="PackageReference"/> unique identifier</param>
        /// <param name="version">The number of versions associated with this application.</param>
        internal PackageReference(string identifier, string version)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new NotImplementedException(nameof(identifier));
            }

            if (string.IsNullOrWhiteSpace(version))
            {
                throw new NotImplementedException(nameof(version));
            }
            this.Identifier = identifier;
            this.Version = version;
        }
    }
}
