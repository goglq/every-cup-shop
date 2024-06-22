using System.Security.Claims;
using AutoMapper;
using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Dtos;
using EveryCupShop.Models;
using EveryCupShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EveryCupShop.Controllers;

[Route("[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;

    private readonly IMapper _mapper;

    private readonly IOrderService _orderService;

    public OrderController(
        ILogger<OrderController> logger,
        IMapper mapper,
        IOrderService orderService)
    {
        _logger = logger;
        _mapper = mapper;
        _orderService = orderService;
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CreateOrderViewModel>> CreateOrder(CreateOrderDto createOrderDto)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                throw new ApiUnauthorizedException();

            var userGuid = Guid.Parse(userId);

            var order = await _orderService.CreateOrder(userGuid, createOrderDto.CupIdsAmount);
            var orderViewModel = _mapper.Map<CreateOrderViewModel>(order);
            
            return Created(Uri.UriSchemeHttp, new ResponseMessage<CreateOrderViewModel>(orderViewModel, true));
        }
        catch (ApiException e)
        {
            _logger.LogInformation(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<AddOrderItemViewModel>> AddOrderItem(AddOrderItemDto addOrderItemDto)
    {
        try
        {
            var orderItem = await _orderService
                .CreateOrderItem(addOrderItemDto.OrderId, addOrderItemDto.CupId, addOrderItemDto.Amount);
            var orderItemViewModel = _mapper.Map<AddOrderItemViewModel>(orderItem);
            return Ok(new ResponseMessage<AddOrderItemViewModel>(orderItemViewModel, true));
        }
        catch (ApiException e)
        {
            Console.WriteLine(e);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }

    [Authorize]
    [HttpPatch]
    public async Task<ActionResult> ChangeOrderState(ChangeOrderStateDto changeOrderStateDto)
    {
        try
        {
            var order = await _orderService.ChangeOrderState(changeOrderStateDto.OrderId, changeOrderStateDto.State);
            var orderViewModel = _mapper.Map<ChangeOrderStateViewModel>(order);
            
            return Ok(new ResponseMessage<ChangeOrderStateViewModel>(orderViewModel, true));
        }
        catch (ApiException e)
        {
            _logger.LogInformation(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }
}