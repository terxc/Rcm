using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Rcm.Shared.Exceptions;
public class ValidationException : Exception
{
    public ValidationException() : base("Ошибка валидации")
    {
        Errors = new List<string>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.Select(x => x.ErrorMessage).ToList();
    }

    public List<string> Errors { get; }
}
