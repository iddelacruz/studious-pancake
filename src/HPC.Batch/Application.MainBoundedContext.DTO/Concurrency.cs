
namespace Application.MainBoundedContext.DTO
{
    using System.Runtime.Serialization;

    [DataContract(Name = "concurrency")]
    public class Concurrency
    {
        [DataMember(Name = "taskSlotsPerNode", Order = 1)]
        public uint? TaskSlotsPerNode { get; set; }

        [DataMember(Name = "policy", Order = 2)]
        public string Policy { get; set; }
    }
}
