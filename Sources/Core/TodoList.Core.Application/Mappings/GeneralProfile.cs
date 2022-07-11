using AutoMapper;
using TodoList.Core.Application.Dtos.Queries;
using TodoList.Core.Domain.Entities;

namespace TodoList.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Todo, TodoQuery>();
        }
    }
}