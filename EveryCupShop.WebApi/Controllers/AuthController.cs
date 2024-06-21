using System.Security.Claims;
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
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    
    private readonly IMapper _mapper;
    
    private readonly ITokenService _tokenService;

    private readonly IUserService _userService;

    private readonly IValidator<CreateUserDto> _createUserValidator;
    
    private readonly IValidator<UserSignInDto> _userLoginValidator;

    public AuthController(
        ILogger<AuthController> logger,
        IMapper mapper, 
        ITokenService tokenService, 
        IUserService userService, 
        IValidator<CreateUserDto> createUserValidator, 
        IValidator<UserSignInDto> userLoginValidator)
    {
        _mapper = mapper;
        _tokenService = tokenService;
        _userService = userService;
        _createUserValidator = createUserValidator;
        _userLoginValidator = userLoginValidator;
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
    public async Task<ActionResult<TokensViewModel>> SignIn(UserSignInDto userSignInDto)
    {
        try
        {
            var validationResult = await _userLoginValidator.ValidateAsync(userSignInDto);

            if (!validationResult.IsValid)
                throw new ApiValidationException();

            var tokens = await _userService.SignIn(userSignInDto.Email, userSignInDto.Password);

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
            
            await _userService.SignOut(userGuid);
            
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