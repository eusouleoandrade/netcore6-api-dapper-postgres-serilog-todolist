using AutoMapper;
using TodoList.Core.Application.Dtos.Queries;
using TodoList.Core.Application.Dtos.Requests;
using TodoList.Core.Application.Dtos.Responses;
using TodoList.Core.Domain.Entities;

namespace TodoList.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Todo, TodoQuery>();

            CreateMap<CreateTodoUseCaseRequest, Todo>();

            CreateMap<Todo, CreateTodoUseCaseResponse>();

            CreateMap<CreateTodoRequest, CreateTodoUseCaseRequest>();

            CreateMap<CreateTodoUseCaseResponse, CreateTodoQuery>();

            CreateMap<Todo, GetTodoUseCaseResponse>();

            CreateMap<GetTodoUseCaseResponse, GetTodoQuery>();
        }
    }
}