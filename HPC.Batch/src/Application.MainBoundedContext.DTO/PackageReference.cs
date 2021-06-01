

namespace Application.MainBoundedContext.DTO
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(Name = "packageReference")]
    public class PackageReference
    {
        [Required]
        [DataMember(Name = "appId", Order = 1)]
        public string Identifier { get; set; }

        [DataMember(Name = "version", Order = 2)]
        public string Version { get; set; }
    }
}
