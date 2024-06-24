using AutoMapper;
using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Models;
using EveryCupShop.Dtos;
using EveryCupShop.Models;
using EveryCupShop.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EveryCupShop.Controllers;

[Authorize(Roles = "Admin")]
[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    private readonly IMapper _mapper;

    private readonly IValidator<CreateUserDto> _createUserValidator;

    private readonly ILogger<UserController> _logger;

    public UserController(
        IUserService userService,
        IValidator<CreateUserDto> createUserValidator,
        IMapper mapper, 
        ILogger<UserController> logger)
    {
        _userService = userService;
        _createUserValidator = createUserValidator;
        _mapper = mapper;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult<IList<UserViewModel>>> GetAll()
    {
        try
        {
            var users = await _userService.GetUsers();
            var userViewModels = _mapper.Map<IList<UserViewModel>>(users);
            return Ok(new ResponseMessage<IList<UserViewModel>>(userViewModels, true));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserViewModel>> Get(Guid id)
    {
        try
        {
            var user = await _userService.GetUser(id);
            var userViewModel = _mapper.Map<UserViewModel>(user);
            return Ok(new ResponseMessage<UserViewModel>(userViewModel, true));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<CreateUserViewModel>> Create(CreateUserDto userDto)
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
    
    [HttpPatch]
    public async Task<ActionResult> ChangeEmail(ChangeUserEmailDto dto)
    {
        try
        {
            var (id, email) = dto;
            var updatedUser = await _userService.ChangeEmail(id, email);
            var updatedUserViewModel = _mapper.Map<ChangeUserEmailViewModel>(updatedUser);
            return Ok(new ResponseMessage<ChangeUserEmailViewModel>(updatedUserViewModel, true));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult> ChangeEmail(ChangeUserPasswordDto dto)
    {
        try
        {
            var (id, password) = dto;
            await _userService.ChangePassword(id, password);
            return Ok(new ResponseMessage<ChangeUserPasswordViewModel>(null, true));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }
    
    [HttpPut]
    public async Task<ActionResult> ChangeUser(ChangeUserDto dto)
    {
        try
        {
            var user = _mapper.Map<User>(dto);
            var updatedUser = await _userService.Change(user);
            var updatedUserViewModel = _mapper.Map<UserViewModel>(updatedUser);
            return Ok(new ResponseMessage<UserViewModel>(updatedUserViewModel, true));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            var deletedUser = await _userService.Delete(id);
            var deletedUserViewModel = _mapper.Map<UserViewModel>(deletedUser);
            return Ok(new ResponseMessage<UserViewModel>(deletedUserViewModel, true));
        }
        catch (ApiException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ResponseMessage<ProblemDetails>(null, false));
        }
    }

    [AllowAnonymous]
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
}