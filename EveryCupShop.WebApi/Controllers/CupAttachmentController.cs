using AutoMapper;
using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Models;
using EveryCupShop.Dtos;
using EveryCupShop.Models;
using EveryCupShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EveryCupShop.Controllers;

[Route("[controller]")]
[ApiController]
public class CupAttachmentController : ControllerBase
{
    private readonly ILogger<CupAttachmentController> _logger;

    private readonly IMapper _mapper;

    private readonly ICupService _cupService;

    public CupAttachmentController(
        ILogger<CupAttachmentController> logger, 
        IMapper mapper, 
        ICupService cupService)
    {
        _logger = logger;
        _mapper = mapper;
        _cupService = cupService;
    }

    [HttpGet]
    public async Task<ActionResult<IList<CupAttachmentViewModel>>> GetAll()
    {
        try
        {
            var cupAttachments = await _cupService.GetCupAttachments();
            var cupAttachmentViewModels =
                _mapper.Map<IList<CupAttachment>, IList<CupAttachmentViewModel>>(cupAttachments);
            return Ok(new ResponseMessage<IList<CupAttachmentViewModel>>(cupAttachmentViewModels, true));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<IList<CupAttachmentViewModel>>> Get(Guid id)
    {
        try
        {
            var cupAttachment = await _cupService.GetCupAttachment(id);
            var cupAttachmentViewModel = _mapper.Map<CupAttachment, CupAttachmentViewModel>(cupAttachment);
            return Ok(new ResponseMessage<CupAttachmentViewModel>(cupAttachmentViewModel, true));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }

    [HttpPost]
    public async Task<ActionResult<CreateCupAttachmentViewModel>> CreateCupAttachment(CreateCupAttachmentDto createCupAttachmentDto)
    {
        try
        {
            var cupAttachment = await _cupService.CreateAttachment(createCupAttachmentDto.Name, createCupAttachmentDto.Description, createCupAttachmentDto.Price, createCupAttachmentDto.Amount);
            var cupAttachmentViewModel = _mapper.Map<CreateCupAttachmentViewModel>(cupAttachment);
            return Ok(new ResponseMessage<CreateCupAttachmentViewModel>(cupAttachmentViewModel, true));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<CupAttachmentViewModel>> Delete(Guid id)
    {
        try
        {
            var cupAttachment = await _cupService.DeleteCupAttachment(id);
            var cupAttachmentViewModel = _mapper.Map<CupAttachmentViewModel>(cupAttachment);
            return Ok(new ResponseMessage<CupAttachmentViewModel>(cupAttachmentViewModel, true));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }
}