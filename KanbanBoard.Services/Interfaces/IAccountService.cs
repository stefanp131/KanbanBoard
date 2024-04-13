using System.Threading.Tasks;
using KanbanBoard.Services.Dtos;

namespace KanbanBoard.Services.Interfaces;

public interface IAccountService
{
    Task LoginAsync(LoginDto loginDto);
    Task RegisterAsync(RegisterDto registerDto);
}