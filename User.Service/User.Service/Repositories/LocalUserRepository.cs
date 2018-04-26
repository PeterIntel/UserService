using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using User.Service.CustomExtensionMethods;
using User.Service.Entities;

namespace User.Service.Repositories
{
    public class LocalUserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly string _jsonFile;

        private List<UserEntity> _users;

        public LocalUserRepository(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _jsonFile = Path.Combine(_hostingEnvironment.ContentRootPath, configuration["LocalStorageJsonFilePath"]);
            _users = ReadUsers();
        }

        private List<UserEntity> ReadUsers()
        {
            List<UserEntity> users;

            try
            {
                var json = File.ReadAllText(_jsonFile);

                users = JsonConvert.DeserializeObject<List<UserEntity>>(json);
            }
            catch (FileNotFoundException)
            {
                users = new List<UserEntity>();
            }

            return users;
        }

        private Task WriteUsersAsync(IList<UserEntity> array)
        {
            string newJsonResult = JsonConvert.SerializeObject(array, Formatting.Indented);
            return File.WriteAllTextAsync(_jsonFile, newJsonResult);
        }

        public async Task<UserEntity> Add(UserEntity user)
        {
            user.Id = Guid.NewGuid();
            _users.Add(user);
            await WriteUsersAsync(_users);

            return await GetSingleById(user.Id);
        }

        public Task Delete(Guid id)
        {
            var userToDeleted = _users.FirstOrException(id);
            _users.Remove(userToDeleted);

            return WriteUsersAsync(_users);
        }

        public async Task<List<UserEntity>> GetAll()
        {
            return _users;
        }

        public async Task<UserEntity> GetSingleById(Guid id)
        {
            return _users.FirstOrException(id);
        }

        public async Task<UserEntity> Update(UserEntity user)
        {
            var userToUpdate = _users.FirstOrException(user.Id);
            _users.Remove(userToUpdate);
            _users.Add(user);
            await WriteUsersAsync(_users);

            return await GetSingleById(user.Id);
        }
    }
}
