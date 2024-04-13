using AutoMapper;
using KanbanBoard.Data.Entities;
using KanbanBoard.Services.Dtos;


namespace KanbanBoard.Services.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<RegisterDto, AppUser>();
    }

}