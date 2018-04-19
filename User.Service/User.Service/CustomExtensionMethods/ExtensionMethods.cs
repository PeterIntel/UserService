using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using User.Service.CustomExceptions;
using User.Service.Entities;

namespace User.Service.CustomExtensionMethods
{
    public static class ExtensionMethods
    {
        public static void ValidateModelState(this UserEntity obj)
        {
            var context = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(obj, context, validationResults))
            {
                throw new ModelStateErrorException(validationResults);
            }
        }

        public static JToken FirstOrException(this JArray array, Func<JToken, bool> predicate)
        {

            var item = array.First(predicate);
            if (item == null)
            {
                throw new NotFoundIdException("Record with such Id was not found");
            }

            return item;
        }
    }
}
