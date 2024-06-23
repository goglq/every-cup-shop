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
public class CupController : ControllerBase
{
    private readonly ILogger<CupController> _logger;

    private readonly IMapper _mapper;

    private readonly ICupService _cupService;

    public CupController(
        ILogger<CupController> logger, 
        IMapper mapper, 
        ICupService cupService)
    {
        _logger = logger;
        _mapper = mapper;
        _cupService = cupService;
    }

    [HttpPost]
    public async Task<ActionResult<CreateCupViewModel>> CreateCup(CreateCupDto createCupDto)
    {
        try
        {
            var cup = await _cupService.CreateCup(createCupDto.CupShapeId, createCupDto.CupAttachmentId);
            var cupViewModel = _mapper.Map<CreateCupViewModel>(cup);
            return Ok(new ResponseMessage<CreateCupViewModel>(cupViewModel, true));
        }
        catch (ApiException e)
        {
            _logger.LogInformation(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult<ChangeCupViewModel>> ChangeCup(ChangeCupDto createCupDto)
    {
        try
        {
            var cup = await _cupService.ChangeCup(createCupDto.CupId, createCupDto.CupShapeId, createCupDto.CupAttachmentId);
            var cupViewModel = _mapper.Map<ChangeCupViewModel>(cup);
            return Ok(new ResponseMessage<ChangeCupViewModel>(cupViewModel, true));
        }
        catch (ApiException e)
        {
            _logger.LogInformation(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }
}