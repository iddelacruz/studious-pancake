namespace Application.MainBoundedContext.DTO
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(Name = "startTask")]
    public class StartTask
    {

        [Required]
        [DataMember(Name = "command", Order = 1)]
        public string Command { get; set; }

        [DataMember(Name = "resources", Order = 1)]
        public ResourceFile Resource { get; set; }

        [DataMember(Name = "maxTaskRetryCount", Order = 2)]
        public int? MaxTaskRetryCount { get; set; }

        [DataMember(Name = "waitForSuccess", Order = 3)]
        public bool? WaitForSuccess { get; set; }
    }
}
