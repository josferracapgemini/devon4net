﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using BaseNameProject.WebAPI.Implementation.Business.TodoManagement.Dto;
using BaseNameProject.WebAPI.Implementation.Business.TodoManagement.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseNameProject.WebAPI.Implementation.Business.TodoManagement.Controllers
{
    /// <summary>
    /// TODOs controller
    /// </summary>
    [EnableCors("CorsPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class TodoController: ControllerBase
    {
        private readonly ITodoService _todoService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="todoService"></param>
        public TodoController( ITodoService todoService)
        {
            _todoService = todoService;
        }


        /// <summary>
        /// Gets the entire list of TODOS
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<TodoDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetTodo()
        {
            Devon4NetLogger.Debug("Executing GetTodo from controller TodoController");
            return Ok(await _todoService.GetTodo().ConfigureAwait(false));
        }

        /// <summary>
        /// Creates the TODO
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(TodoDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Create(string todoDescription)
        {
            Devon4NetLogger.Debug("Executing GetTodo from controller TodoController");
            var result = await _todoService.CreateTodo(todoDescription).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Deletes the TODO provided the id
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(long), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Delete(long todoId)
        {
            Devon4NetLogger.Debug("Executing GetTodo from controller TodoController");
            return Ok(await _todoService.DeleteTodoById(todoId).ConfigureAwait(false));
        }

        /// <summary>
        /// Modifies the done status of the TODO provided the id
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(TodoDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpOptions]
        public async Task<ActionResult> ModifyTodo(long todoId)
        {
            Devon4NetLogger.Debug("Executing ModifyTodo from controller TodoController");
            return Ok(await _todoService.ModifyTodoById(todoId).ConfigureAwait(false));
        }
    }
}
