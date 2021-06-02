
namespace Domain.MainBoundedContext.BatchModule.Aggregates.NodePools
{
    using System;

    public sealed class Details
    {
        public string DisplayName { get; internal set; }

        public Details(string diplayName)
        {
            this.DisplayName = diplayName;
        }
    }
}
