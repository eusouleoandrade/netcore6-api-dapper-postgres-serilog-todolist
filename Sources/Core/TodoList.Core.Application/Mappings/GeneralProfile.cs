using AutoMapper;
using TodoList.Core.Application.Dtos.Models;
using TodoList.Core.Domain.Entities;

namespace TodoList.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Todo, TodoModel>();
        }
    }
}