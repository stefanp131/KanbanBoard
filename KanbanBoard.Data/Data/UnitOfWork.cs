using System.Threading.Tasks;
using KanbanBoard.Data.Interfaces;

namespace KanbanBoard.Data.Data;

public class UnitOfWork(KanbanBoardContext context) : IUnitOfWork
{
    public async Task<int> Complete()
    {
        return await context.SaveChangesAsync();
    }
}