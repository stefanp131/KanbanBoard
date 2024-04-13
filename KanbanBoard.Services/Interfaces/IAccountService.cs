using System.Threading.Tasks;
using KanbanBoard.Services.Dtos;

namespace KanbanBoard.Services.Interfaces;

public interface IAccountService
{
    Task<string> LoginAsync(LoginDto loginDto);
    Task<string> RegisterAsync(RegisterDto registerDto);
}