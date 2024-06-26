using System;
using System.Threading.Tasks;
using KanbanBoard.API.Controllers;
using KanbanBoard.Services.Dtos;
using KanbanBoard.Services.Exceptions;
using KanbanBoard.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
            var token = await _accountService.LoginAsync(loginDto);

            Response.Cookies.Append("jwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                MaxAge = TimeSpan.FromDays(1),
                Secure = true
            });
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
            var token = await _accountService.RegisterAsync(registerDto);

            Response.Cookies.Append("jwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                MaxAge = TimeSpan.FromDays(1),
                Secure = true
            });
            return Ok();
        }
        catch (RegistrationFailedException ex)
        {
            return BadRequest("RegistrationFailed " + ex.Message);
        }
    }
}