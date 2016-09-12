using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace ShortlistManager.Web.Infrastructure.ErrorHandling
{
    public static class DbEntityValidationExceptionExtensions
    {
        public static string ExtractErrorMessage(this DbEntityValidationException dbEntityValidationException)
        {
            var errors = dbEntityValidationException.EntityValidationErrors
                .SelectMany(eve => eve.ValidationErrors, (eve, ve) => $"{ve.PropertyName}: {ve.ErrorMessage}");

            return string.Join(Environment.NewLine, errors);
        }
    }
}