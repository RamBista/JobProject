using System.ComponentModel.DataAnnotations;

namespace JobBackEnd.BLL.Attributes.UnitTest;

public class Tests
{
    private ValidationResult ValidateTimeInterval(string? timeInterval)
    {
        var attribute = new TimeIntervalAttribute();
        return attribute.GetValidationResult(timeInterval, new ValidationContext(timeInterval??string.Empty));
    }


    [Test]
    [TestCase(null, true)]                  // Valid: Null is allowed
    [TestCase("", true)]                    // Invalid: Empty string
    [TestCase("09:00 AM-11:00 AM", true)]   // Valid interval
    [TestCase("09:00 AM-08:00 AM", false)]  // Invalid: Start time is later than end time
    [TestCase("09:00 AM-13:00 PM", false)]  // Invalid: Not in 12 hr format
    [TestCase("09:00 AM-06:00 PM", false)]  // Invalid: Exceed time limit
    [TestCase("09:00 AM-09:00 AM", false)]  // Invalid: Same time
    [TestCase("invalid-format", false)]     // Invalid: Incorrect format
    public void ValidateTimeInterval_TestCases(string timeInterval, bool isValid)
    {
        // Act
        var result = ValidateTimeInterval(timeInterval);

        // Assert
        if (isValid)
            Assert.That(result, Is.EqualTo(ValidationResult.Success), $"Failed for valid time interval: {timeInterval}");
        else
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success), $"Passed for invalid time interval: {timeInterval}");
    }

}
