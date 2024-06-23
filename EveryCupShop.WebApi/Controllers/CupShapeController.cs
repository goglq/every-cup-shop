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
public class CupShapeController : ControllerBase
{
    private readonly ILogger<CupShapeController> _logger;

    private readonly IMapper _mapper;

    private readonly ICupService _cupService;

    public CupShapeController(
        ILogger<CupShapeController> logger, 
        IMapper mapper, 
        ICupService cupService)
    {
        _logger = logger;
        _mapper = mapper;
        _cupService = cupService;
    }

    [HttpGet]
    public async Task<ActionResult<IList<CupShapeViewModel>>> GetAll()
    {
        try
        {
            var cupShapes = await _cupService.GetCupShapes();
            var cupShapeViewModels = _mapper.Map<IList<CupShapeViewModel>>(cupShapes);
            return Ok(new ResponseMessage<IList<CupShapeViewModel>>(cupShapeViewModels, true));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CupShapeViewModel>> Get(Guid id)
    {
        try
        {
            var cupShape = await _cupService.GetCupShape(id);
            var cupShapeViewModel = _mapper.Map<CupShapeViewModel>(cupShape);
            return Ok(new ResponseMessage<CupShapeViewModel>(cupShapeViewModel, true));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<CreateCupShapeViewModel>> CreateCupShape(CreateCupShapeDto createCupShapeDto)
    {
        try
        {
            var cupShape = await _cupService.CreateShape(createCupShapeDto.Name, createCupShapeDto.Description, createCupShapeDto.Price, createCupShapeDto.Amount);
            var cupShapeViewModel = _mapper.Map<CreateCupShapeViewModel>(cupShape);
            return Ok(new ResponseMessage<CreateCupShapeViewModel>(cupShapeViewModel, true));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }
}