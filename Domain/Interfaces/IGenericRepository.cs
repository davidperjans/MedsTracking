﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<OperationResult<IEnumerable<T>>> GetAllAsync();
        Task<OperationResult<T>> GetByIdAsync(string id);
        Task<OperationResult<T>> AddAsync(T entity);
        Task<OperationResult<T>> UpdateAsync(T entity);
        Task<OperationResult<bool>> DeleteAsync(string id);

    }
}
