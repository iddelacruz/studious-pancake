
namespace Application.MainBoundedContext.DTO
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(Name = "poolConfig")]
    public class PoolConfig
    {
        [Required]
        [DataMember(Name = "poolId", Order = 1)]
        public string Identifier { get; set; }

        [Required]
        [DataMember(Name = "dedicatedNodes", Order = 2)]
        public ushort DedicatedNodes { get; set; }

        [DataMember(Name = "lowPriorityNodes", Order = 3)]
        public ushort? LowPriorityNodes { get; set; }

        [DataMember(Name = "packageReference", Order = 4)]
        public PackageReference PackageReference { get; set; }

        [DataMember(Name = "concurrency", Order = 5)]
        public Concurrency Concurrency { get; set; }

        [DataMember(Name = "startTask", Order = 6)]
        public StartTask StartTask { get; set; }
    }
}
