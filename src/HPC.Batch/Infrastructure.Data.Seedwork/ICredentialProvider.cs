
namespace Infrastructure.Data.Seedwork
{
    using System;
    using System.Threading.Tasks;

    public interface ICredentialProvider
    {
        string GetSecret(string secretName);
    }
}
