using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Service.Entities;

namespace User.Service.CustomModelBinders
{
    public class ValidateModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(UserEntity))
                return new ValidateModelBinder();

            return null;
        }
    }
}
