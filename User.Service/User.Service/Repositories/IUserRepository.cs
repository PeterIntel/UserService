using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Service.Entities;

namespace User.Service.Repositories
{
    public interface IUserRepository
    {
        Task<IActionResult> Add(UserEntity user);

        Task<IActionResult> Delete(Guid id);

        Task<IActionResult> GetAll();

        Task<IActionResult> GetSingle(Guid id);

        Task<IActionResult> Update(UserEntity user);
    }
}