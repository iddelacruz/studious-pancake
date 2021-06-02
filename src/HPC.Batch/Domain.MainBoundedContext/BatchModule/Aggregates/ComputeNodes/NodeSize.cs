namespace Domain.MainBoundedContext.BatchModule.Aggregates.ComputeNodes
{
    using System;

    public sealed class NodeSize
    {
        /// <summary>
        /// Get or set the <see cref="NodePool"/> virtual machine size.
        /// </summary>
        public string VirtualMachineSize { get; internal set; }

        internal NodeSize(string virtualMachineSize)
        {
            this.VirtualMachineSize = virtualMachineSize;
        }
    }
}
