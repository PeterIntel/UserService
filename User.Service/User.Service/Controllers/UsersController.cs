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
        public IEnumerable<UserEntity> Get()
        {
            var users = _userRepository.GetAll();

            return users;
        }

        [HttpGet("{id}")]
        public UserEntity Get(Guid id)
        {
            var user = _userRepository.GetSingle(id);

            return user;
        }

        [HttpPost]
        public void Post([FromBody]UserEntity user)
        {
        }

        [HttpPut("{id}")]
        public void Put([FromBody]string user)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}
