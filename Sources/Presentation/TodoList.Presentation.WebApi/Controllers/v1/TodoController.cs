﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoList.Core.Application.Dtos.Queries;
using TodoList.Core.Application.Dtos.Requests;
using TodoList.Core.Application.Dtos.Wrappers;
using TodoList.Core.Application.Interfaces.UseCases;
using TodoList.Infra.Notification.Contexts;
using TodoList.Presentation.WebApi.Controllers.Common;

namespace TodoList.Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TodoController : BaseApiController
    {
        private readonly IGetAllTodoUseCase _getAllTodoUseCase;

        private readonly ICreateTodoUseCase _createTodoUseCase;

        private readonly IDeleteTodoUseCase _deleteTodoUseCase;

        private readonly IMapper _mapper;

        private readonly NotificationContext _notificationContext;

        public TodoController(IGetAllTodoUseCase getAllTodoUseCase,
            ICreateTodoUseCase createTodoUseCase,
            IMapper mapper,
            NotificationContext notificationContext,
            IDeleteTodoUseCase deleteTodoUseCase)
        {
            _getAllTodoUseCase = getAllTodoUseCase;
            _createTodoUseCase = createTodoUseCase;
            _mapper = mapper;
            _notificationContext = notificationContext;
            _deleteTodoUseCase = deleteTodoUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<TodoQuery>>>> Get()
        {
            var useCaseResponse = await _getAllTodoUseCase.RunAsync();

            return Ok(new Response<IReadOnlyList<TodoQuery>>(useCaseResponse, true));
        }

        [HttpPost]
        public async Task<ActionResult<Response<CreateTodoQuery>>> Post([FromBody] CreateTodoRequest request)
        {
            var useCaseRequest = _mapper.Map<CreateTodoUseCaseRequest>(request);
            var useCaseResponse = await _createTodoUseCase.RunAsync(useCaseRequest);

            if (_notificationContext.HasErrorNotification)
                return BadRequest();

            var response = _mapper.Map<CreateTodoQuery>(useCaseResponse);

            return Created($"/api/v1/todo/{response.Id}",
                new Response<CreateTodoQuery>(data: response, succeeded: true));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            await _deleteTodoUseCase.RunAsync(id);

            return NoContent();
        }
    }
}