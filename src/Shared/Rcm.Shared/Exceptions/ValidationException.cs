using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Rcm.Shared.Exceptions;
public class ValidationException : Exception
{
    public ValidationException() : base("Ошибка валидации")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}
