using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using User.Service.Entities;
using User.Service.Repositories;

namespace User.Service.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserRepository _userRepository;

        public UsersController(IUserRepository userRepositury)
        {
            _userRepository = userRepositury;
        }

        [HttpGet]
        public async Task<IEnumerable<UserEntity>> Get()
        {
            var users = await _userRepository.GetAll();

            return users;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userRepository.GetSingle(id);

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserEntity user)
        {
            return await _userRepository.Add(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]UserEntity user)
        {
            return await _userRepository.Update(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await _userRepository.Delete(id);
        }
    }
}
