using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using User.Service.Entities;

namespace User.Service.Repositories
{
    public class LocalUserRepository : IUserRepository
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly string jsonFile;

        public LocalUserRepository(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            jsonFile = Path.Combine(_hostingEnvironment.ContentRootPath, @"AppData\users.json");
        }

        private async Task<JArray> ReadJArray()
        {
            var json = await File.ReadAllTextAsync(jsonFile);

            JArray users = JArray.Parse(json);

            return users;
        }

        private async Task WriteJArray(JArray array)
        {
            string newJsonResult = JsonConvert.SerializeObject(array, Formatting.Indented);
            await File.WriteAllTextAsync(jsonFile, newJsonResult);
        }

        public async Task<IActionResult> Add(UserEntity user)
        {
            var users = await ReadJArray();
            user.Id = Guid.NewGuid();
            var newUser = JObject.FromObject(user);
            users.Add(newUser);
            await WriteJArray(users);

            return new OkResult();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var users = await ReadJArray();
            var userToDeleted = users.FirstOrDefault(user => user["Id"].ToString() == id.ToString());
            users.Remove(userToDeleted);
            await WriteJArray(users);

            return new OkResult();
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            var json = await File.ReadAllTextAsync(jsonFile);

            return JsonConvert.DeserializeObject<IEnumerable<UserEntity>>(json);
        }

        public async Task<IActionResult> GetSingle(Guid id)
        {
            var users = await ReadJArray();
            var user = users.First(u => u["Id"].ToString() == id.ToString());

            return new OkObjectResult(user);
        }

        public async Task<IActionResult> Update(UserEntity user)
        {
            JArray users = await ReadJArray();
            var userToUpdate = users.First(u => u["Id"].ToString() == user.Id.ToString());
            users.Remove(userToUpdate);
            users.Add(JObject.FromObject(user));
            await WriteJArray(users);

            return new OkResult();
        }
    }
}
