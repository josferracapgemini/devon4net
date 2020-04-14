﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.Domain.UnitOfWork.Service;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.Log;
using BaseNameProject.WebAPI.Implementation.Business.TodoManagement.Converters;
using BaseNameProject.WebAPI.Implementation.Business.TodoManagement.Dto;
using BaseNameProject.WebAPI.Implementation.Domain.Database;
using BaseNameProject.WebAPI.Implementation.Domain.Entities;
using BaseNameProject.WebAPI.Implementation.Domain.RepositoryInterfaces;

namespace BaseNameProject.WebAPI.Implementation.Business.TodoManagement.Service
{
    /// <summary>
    /// TODO service implementation
    /// </summary>
    public class TodoService: Service<TodoContext>, ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uoW"></param>
        public TodoService(IUnitOfWork<TodoContext> uoW) : base(uoW)
        {
            _todoRepository = uoW.Repository<ITodoRepository>();
        }

        /// <summary>
        /// Gets the TODO
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TodoDto>> GetTodo(Expression<Func<Todos, bool>> predicate = null)
        {
            Devon4NetLogger.Debug("GetTodo method from service TodoService");
            var result = await _todoRepository.GetTodo(predicate).ConfigureAwait(false);
            return result.Select(TodoConverter.ModelToDto);
        }

        /// <summary>
        /// Gets the TODO by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Todos> GetTodoById(long id)
        {
            Devon4NetLogger.Debug($"GetTodoById method from service TodoService with value : {id}");
            return await _todoRepository.GetTodoById(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates the TODO
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<Todos> CreateTodo(string description)
        {
            Devon4NetLogger.Debug($"SetTodo method from service TodoService with value : {description}");

            if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("The 'Description' field can not be null.");
            }

            return await _todoRepository.Create(description).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Deletes the TODO by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<long> DeleteTodoById(long id)
        {
            Devon4NetLogger.Debug($"DeleteTodoById method from service TodoService with value : {id}");
            var todo = await _todoRepository.GetFirstOrDefault(t => t.Id == id).ConfigureAwait(false);

            if (todo == null)
            {
                throw new ArgumentException($"The provided Id {id} does not exists");
            }

            return await _todoRepository.DeleteTodoById(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Modifies te state of the TODO by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Todos> ModifyTodoById(long id)
        {
            Devon4NetLogger.Debug($"ModifyTodoById method from service TodoService with value : {id}");
            var todo = await _todoRepository.GetFirstOrDefault(t => t.Id == id).ConfigureAwait(false);
            todo.Done = !todo.Done;

            if (todo == null)
            {
                throw new ArgumentException($"The provided Id {id} does not exists");
            }

            return await _todoRepository.Update(todo).ConfigureAwait(false);
        }
    }
}
