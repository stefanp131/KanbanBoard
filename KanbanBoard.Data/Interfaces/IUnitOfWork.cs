using System.Threading.Tasks;

namespace KanbanBoard.Data.Interfaces;

public interface IUnitOfWork
{
    Task<int> Complete();
}