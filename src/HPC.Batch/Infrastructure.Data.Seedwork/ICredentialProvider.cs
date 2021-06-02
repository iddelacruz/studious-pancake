
namespace Infrastructure.Data.Seedwork
{
    using System;
    using System.Threading.Tasks;

    public interface ICredentialProvider
    {
        Task<string> GetSecretAsync(string secretName);
    }
}
