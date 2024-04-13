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
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _unitOfWork = unitOfWork;
    }

    public async Task LoginAsync(LoginDto loginDto)
    {
        var user = await this._userManager.Users
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

        if (user == null) throw new BadCredentialsException();

        var result = await this._signInManager.CheckPasswordSignInAsync
            (user, loginDto.Password, false);

        if (!result.Succeeded) throw new BadCredentialsException();

        await _signInManager.SignInAsync(user, true, CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public async Task RegisterAsync(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) throw new RegistrationFailedException("Username taken");

        var user = this._mapper.Map<AppUser>(registerDto);

        user.UserName = registerDto.Username.ToLower();

        var result = await this._userManager.CreateAsync(user, registerDto.Password);

        var roleResult = await this._userManager.AddToRoleAsync(user, "Member");

        if (!roleResult.Succeeded) throw new RegistrationFailedException(roleResult.Errors.ToString());

        if (!result.Succeeded) throw new RegistrationFailedException(result.Errors.ToString());

        await _signInManager.SignInAsync(user, true, CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private async Task<bool> UserExists(string username)
    {
        return await this._userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
}