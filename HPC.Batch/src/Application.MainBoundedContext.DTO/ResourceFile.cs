namespace Application.MainBoundedContext.DTO
{
    using System.Runtime.Serialization;

    [DataContract(Name = "resources")]
    public class ResourceFile
    {
        [DataMember(Name = "containerName", Order = 1)]
        public string ContainerName { get; set; }

        [DataMember(Name = "blobName", Order = 2)]
        public string BlobName { get; set; }
    }
}
