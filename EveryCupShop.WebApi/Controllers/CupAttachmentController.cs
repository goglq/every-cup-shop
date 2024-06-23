using AutoMapper;
using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Services;
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
            _logger.LogInformation(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }
}