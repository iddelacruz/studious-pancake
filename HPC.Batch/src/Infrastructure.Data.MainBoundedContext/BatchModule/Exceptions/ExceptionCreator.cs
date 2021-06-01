
namespace Infrastructure.Data.MainBoundedContext.BatchModule.Exceptions
{
    using System;
    using Domain.Seedwork.Exceptions;
    using Microsoft.Azure.Batch.Common;

    public class ExceptionCreator
    {
        public ExceptionCreator()
        {
        }

        public BusinessException Throw(BatchException ex)
        {
            throw new NotImplementedException();
        }
    }
}
