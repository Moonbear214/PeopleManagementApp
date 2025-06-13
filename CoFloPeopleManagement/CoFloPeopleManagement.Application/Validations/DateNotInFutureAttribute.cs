using System.ComponentModel.DataAnnotations;

namespace CoFloPeopleManagement.Application.Validations;

public class DateNotInFutureAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is DateTime dateValue)
        {
            return dateValue.Date <= DateTime.Now.Date;
        }
        return true; 
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} cannot be a future date.";
    }
}