using AutoMapper;
using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Models;
using EveryCupShop.Dtos;
using EveryCupShop.Models;
using EveryCupShop.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EveryCupShop.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    private readonly IMapper _mapper;

    private readonly IValidator<CreateUserDto> _createUserValidator;

    private readonly ILogger<UserController> _logger;
    
    public UserController(IUserService userService, IMapper mapper, IValidator<CreateUserDto> createUserValidator, ILogger<UserController> logger)
    {
        _userService = userService;
        _mapper = mapper;
        _createUserValidator = createUserValidator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IList<User>>> GetAll()
    {
        var users = await _userService.GetUsers();
        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> Get(Guid id)
    {
        var user = await _userService.GetUser(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Create(CreateUserDto userDto)
    {
        try
        {
            _logger.LogInformation("Creating a new user");
            var validationResult = await _createUserValidator.ValidateAsync(userDto);

            if (!validationResult.IsValid)
            {
                throw new ApiValidationException();
            }

            var newUser = await _userService.CreateUser(userDto.Email, userDto.Password);
            var newUserViewModel = _mapper.Map<CreateUserViewModel>(newUser);
            return Created(Uri.UriSchemeHttp, new ResponseMessage<CreateUserViewModel>(newUserViewModel, true, "User is created"));
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false, ex.Message));
        }
    }

    [HttpGet("check-email")]
    public async Task<ActionResult<bool>> Check(string email)
    {
        try
        {
            var isEmailAvailable = await _userService.CheckEmail(email);
            var checkEmailViewModel = _mapper.Map<CheckEmailViewModel>(isEmailAvailable);
            return Ok(new ResponseMessage<CheckEmailViewModel>(checkEmailViewModel, true));
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false, ex.Message));
        }
    }

    [HttpGet("refresh-tokens")]
    public async Task<ActionResult<TokensViewModel>> RefreshToken()
    {
        throw new NotImplementedException();
    }
}