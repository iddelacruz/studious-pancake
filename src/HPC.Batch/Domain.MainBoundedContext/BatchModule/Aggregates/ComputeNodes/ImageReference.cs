namespace Domain.MainBoundedContext.BatchModule.Aggregates.ComputeNodes
{
    using System;

    public sealed class ImageReference
    {
        public string Publisher { get; internal set; }

        public string Offer { get; internal set; }

        public string Sku { get; internal set; }

        public string Version { get; internal set; }

        internal ImageReference(string publisher, string offer, string sku, string version)
        {
            this.Publisher = publisher;
            this.Offer = offer;
            this.Sku = sku;
            this.Version = version;
        }
    }
}
