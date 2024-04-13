using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using KanbanBoard.Data.Entities;
using KanbanBoard.Data.Interfaces;
using KanbanBoard.Services.Dtos;
using KanbanBoard.Services.Exceptions;
using KanbanBoard.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace KanbanBoard.Services.Services;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        ITokenGenerator tokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenGenerator = tokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        var user = await this._userManager.Users
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

        if (user == null || !(await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false)).Succeeded)
        {
            throw new BadCredentialsException();
        }

        return await _tokenGenerator.CreateToken(user);
    }

    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) throw new RegistrationFailedException("Username taken");

        var user = this._mapper.Map<AppUser>(registerDto);

        user.UserName = registerDto.Username.ToLower();

        var result = await this._userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded) throw new RegistrationFailedException(result.Errors.ToString());


        var roleResult = await this._userManager.AddToRoleAsync(user, "Member");

        if (!roleResult.Succeeded) throw new RegistrationFailedException(roleResult.Errors.ToString());

        return await _tokenGenerator.CreateToken(user);
    }

    private async Task<bool> UserExists(string username)
    {
        return await this._userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
}