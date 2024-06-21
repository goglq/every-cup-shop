using AutoMapper;
using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Models;
using EveryCupShop.Dtos;
using EveryCupShop.Models;
using EveryCupShop.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EveryCupShop.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    private readonly ITokenService _tokenService;

    private readonly IMapper _mapper;

    private readonly IValidator<CreateUserDto> _createUserValidator;
    
    private readonly IValidator<UserLoginDto> _userLoginValidator;

    private readonly ILogger<UserController> _logger;
    
    public UserController(IUserService userService, ITokenService tokenService, IValidator<CreateUserDto> createUserValidator, IValidator<UserLoginDto> userLoginValidator, IMapper mapper, ILogger<UserController> logger)
    {
        _userService = userService;
        _tokenService = tokenService;
        _createUserValidator = createUserValidator;
        _userLoginValidator = userLoginValidator;
        _mapper = mapper;
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
                throw new ApiValidationException();

            var newUser = await _userService.CreateUser(userDto.Email, userDto.Password);
            var newUserViewModel = _mapper.Map<CreateUserViewModel>(newUser);
            return Created(Uri.UriSchemeHttp, new ResponseMessage<CreateUserViewModel>(newUserViewModel, true, "User is created"));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false, e.Message));
        }
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<TokensViewModel>> SignIn(UserLoginDto userLoginDto)
    {
        try
        {
            var validationResult = await _userLoginValidator.ValidateAsync(userLoginDto);

            if (!validationResult.IsValid)
                throw new ApiValidationException();

            var tokens = await _userService.SignIn(userLoginDto.Email, userLoginDto.Password);
            var tokensViewModel = _mapper.Map<TokensViewModel>(tokens);

            return Ok(new ResponseMessage<TokensViewModel>(tokensViewModel, true, "Successfully signed in"));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return Unauthorized(new ResponseMessage<ProblemDetails>(null, false, e.Message));
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
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false, e.Message));
        }
    }

    [HttpPost("refresh-tokens")]
    public async Task<ActionResult<TokensViewModel>> RefreshToken(RefreshTokenDto refreshTokenDto)
    {
        try
        {
            var tokens = await _tokenService.RefreshTokens(refreshTokenDto.RefreshToken);
            var tokensViewModel = _mapper.Map<TokensViewModel>(tokens);
            return Ok(new ResponseMessage<TokensViewModel>(tokensViewModel, true));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return Unauthorized(new ResponseMessage<ProblemDetails>(null, false, e.Message));
        }
    }
}