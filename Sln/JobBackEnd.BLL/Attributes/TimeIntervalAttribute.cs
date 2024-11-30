using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace JobBackEnd.BLL.Attributes;

public class TimeIntervalAttribute : ValidationAttribute
{
    private const string TimeFormat = "h:mm tt";
    private const int MaxDurationInHours = 5;

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null || string.IsNullOrWhiteSpace(value.ToString()))
            return ValidationResult.Success; // Use [Required] for non-null enforcement.

        string input = value.ToString();
        string invalidMessage = $"Invalid format for {validationContext.DisplayName}. It must follow 12 hours format as 'T1:00 AM - T2:00 AM'.";

        string[] parts = input.Split('-');
        if (parts.Length != 2)
            return new ValidationResult(invalidMessage);

        string startTimeString = parts[0].Trim();
        string endTimeString = parts[1].Trim();

        if (!DateTime.TryParseExact(startTimeString, TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startTime))
            return new ValidationResult(invalidMessage);

        if (!DateTime.TryParseExact(endTimeString, TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endTime))
            return new ValidationResult(invalidMessage);

        if (startTime >= endTime)
            return new ValidationResult($"Invalid input for {validationContext.DisplayName}. The start time must be earlier than end time.");

        TimeSpan duration = endTime - startTime;

        if (duration.TotalHours > MaxDurationInHours)
            return new ValidationResult($"Invalid input for {validationContext.DisplayName}. The max limitation is {MaxDurationInHours} hours.");

        return ValidationResult.Success;
    }
}
