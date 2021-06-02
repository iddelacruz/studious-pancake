namespace DistributedService.WebAPI.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.MainBoundedContext.BatchModule;
    using Application.MainBoundedContext.DTO;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("batch")]
    public class BatchController : ControllerBase
    {
        private readonly IBatchExecutor batchService;

        public BatchController(IBatchExecutor batchService)
        {
            this.batchService = batchService ?? throw new ArgumentNullException(nameof(batchService));
        }

        [HttpPost]
        public async Task<IActionResult> Post(BatchExecutorConfig dto)
        {
            if(dto is null)
            {
                return BadRequest();
            }
            try
            {
                await this.batchService.RunAsync(dto);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
