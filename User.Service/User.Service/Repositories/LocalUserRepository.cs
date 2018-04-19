using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using User.Service.CustomExceptions;
using User.Service.Entities;
using User.Service.CustomExtensionMethods;

namespace User.Service.Repositories
{
    public class LocalUserRepository : IUserRepository
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly string _jsonFile;

        private JArray _users;

        public LocalUserRepository(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _jsonFile = Path.Combine(_hostingEnvironment.ContentRootPath, @"AppData\users.json");
            _users = ReadJArray();
        }

        private JArray ReadJArray()
        {
            var json = File.ReadAllText(_jsonFile);

            JArray users = JArray.Parse(json);
            var usesr = JsonConvert.DeserializeObject<List<UserEntity>>(json);

            return users;
        }

        private async Task WriteJArray(JArray array)
        {
            string newJsonResult = JsonConvert.SerializeObject(array, Formatting.Indented);
            await File.WriteAllTextAsync(_jsonFile, newJsonResult);
        }

        public async Task<IActionResult> Add(UserEntity user)
        {
            try
            {
                user.ValidateModelState();
                user.Id = Guid.NewGuid();
                var newUser = JObject.FromObject(user);
                _users.Add(newUser);
                await WriteJArray(_users);

                return new OkResult();
            }
            catch(ModelStateErrorException ex)
            {
                return new BadRequestObjectResult(ex.ValidationResults);
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var userToDeleted = _users.FirstOrException(user => user["Id"].ToString() == id.ToString());
                _users.Remove(userToDeleted);
                await WriteJArray(_users);

                return new OkResult();
            }
            catch
            {
                return new BadRequestResult();
            }
        }

        public async Task<IActionResult> GetAll()
        {
            var json = await File.ReadAllTextAsync(_jsonFile);

            return new OkObjectResult(JArray.Parse(json));
        }

        public async Task<IActionResult> GetSingle(Guid id)
        {
            var user = _users.First(u => u["Id"].ToString() == id.ToString());

            return new OkObjectResult(user);
        }

        public async Task<IActionResult> Update(UserEntity user)
        {
            try
            {
                user.ValidateModelState();

                var userToUpdate = _users.FirstOrException(u => u["Id"].ToString() == user.Id.ToString());
                _users.Remove(userToUpdate);
                _users.Add(JObject.FromObject(user));
                await WriteJArray(_users);

                return new OkResult();
            }
            catch (ModelStateErrorException ex)
            {
                return new BadRequestObjectResult(ex.ValidationResults);
            }
            catch(NotFoundIdException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                if(user.Id == null)
                {
                    return new BadRequestObjectResult("Null Id Exception");
                }

                return new BadRequestObjectResult(ex.Message);
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
