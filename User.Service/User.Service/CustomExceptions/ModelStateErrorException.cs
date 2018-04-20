using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace User.Service.CustomExceptions
{
    public class ModelStateErrorException : Exception
    {
        public IList<ValidationResult> ValidationResults { get; }

        public ModelStateErrorException(IList<ValidationResult> validationResults)
        {
            ValidationResults = validationResults;
        }
    }
}
