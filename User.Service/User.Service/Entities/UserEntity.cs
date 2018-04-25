using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using User.Service.CustomModelBinders;

namespace User.Service.Entities
{
    [ModelBinder(BinderType = typeof(ValidateModelBinder))]
    public class UserEntity
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
