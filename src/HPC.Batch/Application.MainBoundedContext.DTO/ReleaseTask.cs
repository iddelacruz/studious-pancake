namespace Application.MainBoundedContext.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(Name = "releaseTask")]
    public class ReleaseTask
    {
        [Required]
        [DataMember(Name = "command", Order = 1)]
        public string Command { get; set; }

        [DataMember(Name = "resource", Order = 2)]
        public ResourceFile ResourceFile { get; set; }

        [DataMember(Name = "maxWallClockTime", Order = 3)]
        public DateTime? MaxWallClockTime { get; set; }

        [DataMember(Name = "retentionTime", Order = 4)]
        public DateTime? RetentionTime { get; set; }
    }
}
