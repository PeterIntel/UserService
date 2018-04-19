using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Service.Sevices
{
    public interface IUserService<T>
    {
        Task<IActionResult> Add(T user);

        Task<IActionResult> Delete(Guid id);

        Task<IActionResult> GetAll();

        Task<IActionResult> GetSingle(Guid id);

        Task<IActionResult> Update(T user);
    }
}
