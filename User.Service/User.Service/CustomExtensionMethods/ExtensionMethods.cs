using Microsoft.EntityFrameworkCore;
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

        public static UserEntity FirstOrException(this IList<UserEntity> array, Guid id)
        {
            var item = array.FirstOrDefault(entity => entity.Id.ToString() == id.ToString());
            if (item == null)
            {
                throw new NotFoundIdException("Record with such Id was not found");
            }

            return item;
        }

        public static async Task<UserEntity> FirstOrExceptionAsync<TEntity>(this DbSet<UserEntity> set, Guid id)
        {
            var item = await set.FirstOrDefaultAsync(entity => entity.Id == id);
            if (item == null)
            {
                throw new NotFoundIdException("Record with such Id was not found");
            }

            return item;
        }
    }
}
