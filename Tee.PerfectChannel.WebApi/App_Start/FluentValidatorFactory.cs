using System;
using FluentValidation;
using FluentValidation.Attributes;

namespace Tee.PerfectChannel.WebApi
{
    public class FluentValidatorFactory : IValidatorFactory
    {
        public IValidator<T> GetValidator<T>()
        {
            return (IValidator<T>)GetValidator(typeof(T));
        }

        public IValidator GetValidator(Type type)
        {
            if (type == null)
            {
                return null;
            }

            var validatorAttribute = (ValidatorAttribute)Attribute.GetCustomAttribute(type, typeof(ValidatorAttribute));
            if (validatorAttribute == null || validatorAttribute.ValidatorType == null)
            {
                return null;
            }

            return (IValidator)Activator.CreateInstance(validatorAttribute.ValidatorType);
        }
    }
}