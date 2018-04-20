using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace User.Service.Services
{
    public interface IEntityService<T>
    {
        Task<IActionResult> Add(T entity);

        Task<IActionResult> Delete(Guid id);

        Task<IActionResult> GetAll();

        Task<IActionResult> GetSingleById(Guid id);

        Task<IActionResult> Update(T entity);
    }
}
