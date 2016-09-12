using System;
using System.Collections.Generic;
using FluentValidation;
using ShortlistManager.Services.Models;
using ShortlistManager.Web.Models;

namespace ShortlistManager.Web.Infrastructure.Validation
{
    public class ValidatorFactory : IValidatorFactory
    {
        private readonly Dictionary<Type, IValidator> _validators = new Dictionary<Type, IValidator>();

        public ValidatorFactory()
        {
            // Register validators...
            _validators.Add(typeof(PlayerDto), new PlayerValidator());
            _validators.Add(typeof(LogInModel), new LoginModelValidator());
        }


        public IValidator<T> GetValidator<T>()
        {
            return (IValidator<T>)this.GetValidator(typeof(T));
        }

        public IValidator GetValidator(Type type)
        {
            IValidator validator;

            if (_validators.TryGetValue(type, out validator))
            {
                return validator;
            }
        
            return new NoValidator();
        }        

    }
}