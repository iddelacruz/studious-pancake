
namespace Application.MainBoundedContext.DTO
{
    using System.Runtime.Serialization;
        
    [DataContract(Name = "nodesConfig")]
    public class NodesConfig
    {
        [DataMember(Name = "nodeAgentSkuId", Order = 1)]
        public string NodeAgentSkuId { get; set; }

        [DataMember(Name = "virtualMachineSize", Order = 2)]
        public string VirtualMachineSize { get; set; }

        [DataMember(Name = "publisher", Order = 3)]
        public string Publisher { get; set; }

        [DataMember(Name = "offer", Order = 4)]
        public string Offer { get; set; }

        [DataMember(Name = "sku", Order = 5)]
        public string Sku { get; set; }

        [DataMember(Name = "version", Order = 6)]
        public string Version { get; set; }
    }
}
