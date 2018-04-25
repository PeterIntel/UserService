using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using User.Service.CustomExceptions;
using User.Service.Entities;

namespace User.Service.CustomModelBinders
{
    public class ValidateModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            using (var reader = new StreamReader(bindingContext.HttpContext.Request.Body))
            {
                string body = reader.ReadToEnd();

                if (String.IsNullOrEmpty(body))
                    return Task.CompletedTask;

                var userDto = JsonConvert.DeserializeObject<UserDto>(body);
                UserEntity user = new UserEntity();
                Guid id;

                user.Id = Guid.TryParse(userDto.Id, out id) ? id: Guid.Empty;
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;

                bindingContext.Result = ModelBindingResult.Success(user);

                return Task.CompletedTask;
            }
        }
    }
}
