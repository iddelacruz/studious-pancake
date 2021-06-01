
namespace Application.MainBoundedContext.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(Name = "jobsConfig")]
    public class JobsConfig
    {
        [Required]
        [DataMember(Name = "prefix", Order = 1)]
        public string Prefix { get; set; }

        [Required]
        [DataMember(Name = "tasksPerJob", Order = 2)]
        public uint TasksPerJob { get; set; }

        [DataMember(Name = "maxTaskRetryCount", Order = 3)]
        public int? MaxTaskRetryCount { get; set; }

        [DataMember(Name = "maxWallClockTime", Order = 4)]
        public DateTime? MaxWallClockTime { get; set; }

        [DataMember(Name = "taskFailedAction", Order = 5)]
        public byte? TaskFailedAction { get; set; }

        [DataMember(Name = "preparationTask", Order = 6)]
        public PreparationTask PreparationTask { get; set; }

        [DataMember(Name = "releaseTask", Order = 7)]
        public ReleaseTask ReleaseTask { get; set; }
    }
}
