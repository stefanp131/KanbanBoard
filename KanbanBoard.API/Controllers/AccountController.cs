using System.Threading.Tasks;
using KanbanBoard.API.Controllers;
using KanbanBoard.Services.Dtos;
using KanbanBoard.Services.Exceptions;
using KanbanBoard.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Spaces.API.Controllers;

public class AccountController : BaseApiController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<bool>> Login(LoginDto loginDto)
    {
        try
        {
            await _accountService.LoginAsync(loginDto);
            return Ok();
        }
        catch (BadCredentialsException)
        {
            return Unauthorized("Wrong credentials");
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<bool>> Register(RegisterDto registerDto)
    {
        try
        {
            await _accountService.RegisterAsync(registerDto);
            return Ok();
        }
        catch (RegistrationFailedException ex)
        {
            return BadRequest("RegistrationFailed " + ex.Message);
        }
    }
}