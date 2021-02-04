using dotNet_GMZ_backend.CQRS.Commands.NewsRecordCommands.Create;
using dotNet_GMZ_backend.CQRS.Commands.NewsRecordCommands.Remove;
using dotNet_GMZ_backend.CQRS.Querys.NewsRecordQuerys.Find;
using dotNet_GMZ_backend.Models.ModelsDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using dotNet_GMZ_backend.CQRS.Querys.NewsRecordQuerys.GetAll;

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

        [Route("getAll")]
        [HttpGet]
        //GET : api/RecordNews/getAll
        public async Task<IActionResult> GetAllNewsRecord()
        {
            try
            {
                _logger.LogInformation(nameof(RecordNewsController.GetAllNewsRecord));
                var result = await _mediator.Send(new GetAllNewsRecord());
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(RecordNewsController), e);
                return BadRequest("Error");
            }
        }

        [Route("find")]
        [HttpGet]
        //Get : /api/RecordNews/find
        public async Task<IActionResult> FindNewsRecord(Guid id)
        {
            try
            {
                _logger.LogInformation(nameof(RecordNewsController.FindNewsRecord));
                var result = await _mediator.Send(new FindNewsRecord(id));
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(RecordNewsController.FindNewsRecord), e);
                return BadRequest();
            }
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
                return BadRequest("Error");
            }
        }

        [Route("remove")]
        [HttpPost]
        //POST : /api/RecordNews/remove
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