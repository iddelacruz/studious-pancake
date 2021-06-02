using System;
namespace Domain.Seedwork.Exceptions
{
    //https://docs.microsoft.com/en-us/troubleshoot/iis/http-status-code
    public enum ErrorCode
    {
        //The Batch service is currently unable to receive requests.
        ServerBusy = 50013
    }
}
