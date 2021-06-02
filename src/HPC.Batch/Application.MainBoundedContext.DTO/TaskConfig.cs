namespace Application.MainBoundedContext.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(Name = "tasksConfig")]
    public class TaskConfig
    {
        [Required]
        [DataMember(Name = "prefix", Order = 1)]
        public string Prefix { get; set; }

        [Required]
        [DataMember(Name = "command", Order = 2)]
        public string Command { get; set; }

        [DataMember(Name = "requiredSlots", Order = 3)]
        public ushort RequiredSlots { get; set; }

        [DataMember(Name = "packageReference", Order = 4)]
        public PackageReference PackageReference { get; set; }

        [DataMember(Name = "maxTaskRetryCount", Order = 5)]
        public int? MaxTaskRetryCount { get; set; }

        [DataMember(Name = "maxWallClockTime", Order = 6)]
        public DateTime? MaxWallClockTime { get; set; }

        [DataMember(Name = "retentionTime", Order = 7)]
        public DateTime? RetentionTime { get; set; }
    }
}
