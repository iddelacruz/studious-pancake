namespace Application.MainBoundedContext.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(Name = "preparationTask")]
    public class PreparationTask
    {
        [Required]
        [DataMember(Name = "command", Order = 1)]
        public string Command { get; set; }

        [DataMember(Name = "maxTaskRetryCount", Order = 2)]
        public int? MaxTaskRetryCount { get; set; }

        [DataMember(Name = "maxWallClockTime", Order = 3)]
        public DateTime? MaxWallClockTime { get; set; }

        [DataMember(Name = "retentionTime", Order = 4)]
        public DateTime? RetentionTime { get; set; }

        [DataMember(Name = "resource", Order = 5)]
        public ResourceFile ResourceFile { get; set; }

        [DataMember(Name = "waitForSuccess", Order = 6)]
        public bool? WaitForSuccess { get; set; }

        [DataMember(Name = "reRunOnComputeNodeRebootAfterSuccess", Order = 7)]
        public bool? ReRunOnComputeNodeRebootAfterSuccess { get; set; }
    }
}
