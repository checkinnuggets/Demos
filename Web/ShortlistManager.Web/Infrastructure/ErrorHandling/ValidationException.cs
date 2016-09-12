using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortlistManager.Web.Infrastructure.ErrorHandling
{
    public class ValidationException : ApplicationException
    {
        private readonly string[] _errorMessage;

        public ValidationException(string errorMessage)
        {
            _errorMessage = new[] {errorMessage};
        }

        public ValidationException(IEnumerable<string> errorMessage)
        {
            _errorMessage = errorMessage.ToArray();
        }

        public override string Message => string.Join(Environment.NewLine, _errorMessage);
    }

}