using System.Security.Claims;
using AutoMapper;
using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Dtos;
using EveryCupShop.Models;
using EveryCupShop.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EveryCupShop.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    
    private readonly IMapper _mapper;

    private readonly IAuthService _authService;

    private readonly IValidator<CreateUserDto> _createUserValidator;
    
    private readonly IValidator<UserSignInDto> _userLoginValidator;

    public AuthController(
        ILogger<AuthController> logger,
        IMapper mapper,
        IAuthService authService,
        IValidator<CreateUserDto> createUserValidator, 
        IValidator<UserSignInDto> userLoginValidator)
    {
        _mapper = mapper;
        _createUserValidator = createUserValidator;
        _userLoginValidator = userLoginValidator;
        _authService = authService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<CreateUserViewModel>> SignUp(CreateUserDto userDto)
    {
        try
        {
            _logger.LogInformation("Creating a new user");
            var validationResult = await _createUserValidator.ValidateAsync(userDto);

            if (!validationResult.IsValid)
                throw new ApiValidationException();

            var newUser = await _authService.SignUp(userDto.Email, userDto.Password);
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
    public async Task<ActionResult<TokensViewModel>> SignIn(UserSignInDto userSignInDto)
    {
        try
        {
            var validationResult = await _userLoginValidator.ValidateAsync(userSignInDto);

            if (!validationResult.IsValid)
                throw new ApiValidationException();

            var tokens = await _authService.SignIn(userSignInDto.Email, userSignInDto.Password);

            var tokensViewModel = _mapper.Map<TokensViewModel>(tokens);

            return Ok(new ResponseMessage<TokensViewModel>(tokensViewModel, true, "Successfully signed in"));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return Unauthorized(new ResponseMessage<ProblemDetails>(null, false, e.Message));
        }
    }

    [HttpDelete]
    public async Task<ActionResult> LogOut()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                throw new ApiUnauthorizedException();

            var userGuid = Guid.Parse(userId);
            
            await _authService.SignOut(userGuid);
            
            return Ok(new ResponseMessage<object>(null, true));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false, e.Message));
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult<TokensViewModel>> RefreshToken(RefreshTokenDto refreshTokenDto)
    {
        try
        {
            var tokens = await _authService.Refresh(refreshTokenDto.RefreshToken);
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