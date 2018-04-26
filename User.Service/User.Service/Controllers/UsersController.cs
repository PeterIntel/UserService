using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using User.Service.Entities;
using User.Service.Services;

namespace User.Service.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IEntityService<UserEntity> _userService;

        public UsersController(IEntityService<UserEntity> userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await _userService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return await _userService.GetSingleById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserEntity user)
        {
            return await _userService.Add(user);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UserEntity user)
        {
            return await _userService.Update(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await _userService.Delete(id);
        }
    }
}
