//Copyright (c) Microsoft Corporation

namespace Infrastructure.Data.MainBoundedContext.BatchModule.Monitoring
{
    using System;
    using Microsoft.Azure.Batch;

    // Provides ODATADetailLevel objects for task state queries, both initial (all
    // entities) and delta (entities changed since a given time).
    internal static class DetailLevels
    {
        internal static class IdAndState
        {
            internal static readonly ODATADetailLevel AllEntities = new ODATADetailLevel(selectClause: "id,state");

            internal static ODATADetailLevel OnlyChangedAfter(DateTime time)
            {
                return new ODATADetailLevel(
                    selectClause: "id, state",
                    filterClause: string.Format("stateTransitionTime gt DateTime'{0:o}'", time)
                );
            }
        }
    }
}
