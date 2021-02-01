using dotNet_GMZ_backend.CQRS.Commands.NewsRecordCommands.Create;
using dotNet_GMZ_backend.CQRS.Commands.NewsRecordCommands.Remove;
using dotNet_GMZ_backend.Models.ModelsDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace dotNet_GMZ_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordNewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RecordNewsController> _logger;

        public RecordNewsController(IMediator mediator, ILogger<RecordNewsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Route("create")]
        [HttpPost]
        //POST : /api/RecordNews/create
        public async Task<IActionResult> CreateNewsRecord(CreateNewsRecordDTO newsRecordDto)
        {
            try
            {
                _logger.LogInformation(nameof(RecordNewsController.CreateNewsRecord));
                if (TryValidateModel(newsRecordDto))
                {
                    var result = await _mediator.Send(new CreateNewsRecord(newsRecordDto));
                    if (result)
                    {
                        return Ok();
                    }
                    return BadRequest();
                }
                _logger.LogError(nameof(RecordNewsController.CreateNewsRecord));
                return BadRequest("Error");
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(RecordNewsController.CreateNewsRecord), e);
                return BadRequest(e.Message);
            }
        }

        [Route("remove")]
        [HttpPost]
        // POST : /api/RecordNews/remove
        public async Task<IActionResult> RemoveNewsRecord(RemoveNewsRecordDTO removeNewsRecordDto)
        {
            try
            {
                _logger.LogInformation(nameof(RecordNewsController.RemoveNewsRecord));
                if (TryValidateModel(removeNewsRecordDto))
                {
                    var result = await _mediator.Send(new RemoveNewsRecord(removeNewsRecordDto));
                    if (result)
                    {
                        return Ok();
                    }
                    return BadRequest();
                }
                _logger.LogError(nameof(RecordNewsController.RemoveNewsRecord));
                return BadRequest("Error");
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(RecordNewsController.RemoveNewsRecord), e);
                return BadRequest("Error");
            }
        }
    }
}