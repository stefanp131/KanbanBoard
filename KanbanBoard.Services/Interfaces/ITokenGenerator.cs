using System.Threading.Tasks;
using KanbanBoard.Data.Entities;

namespace KanbanBoard.Services.Interfaces;

public interface ITokenGenerator
{
    Task<string> CreateToken(AppUser user);
}