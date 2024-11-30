using System.ComponentModel.DataAnnotations;

namespace JobBackEnd.BLL.Dtos.UnitTest;

public class CandidateDtoValidationTests
{
    private IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);
        return validationResults;
    }

    [Test]
    public void CandidateDto_ShouldBeValid_WhenAllRequiredFieldsAreProvided()
    {
        // Arrange
        var candidate = new CandidateDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Comment = "Great candidate."
        };

        // Act
        var validationResults = ValidateModel(candidate);

        // Assert
        Assert.IsEmpty(validationResults); // No validation errors
    }

    [Test]
    public void CandidateDto_ShouldBeInvalid_WhenRequiredFieldsAreMissing()
    {
        // Arrange
        var candidate = new CandidateDto
        {
            // Missing required fields: FirstName, LastName, Email, Comment
            PhoneNumber = "1234567890",
            LinkedInProfileUrl = "https://linkedin.com/in/johndoe",
            GitHubProfileUrl = "https://github.com/johndoe"
        };

        // Act
        var validationResults = ValidateModel(candidate);

        // Assert
        Assert.IsNotEmpty(validationResults);
        Assert.That(validationResults, Has.One.Matches<ValidationResult>(v => v.MemberNames.Contains("FirstName")));
        Assert.That(validationResults, Has.One.Matches<ValidationResult>(v => v.MemberNames.Contains("LastName")));
        Assert.That(validationResults, Has.One.Matches<ValidationResult>(v => v.MemberNames.Contains("Email")));
        Assert.That(validationResults, Has.One.Matches<ValidationResult>(v => v.MemberNames.Contains("Comment")));
    }

    [Test]
    public void CandidateDto_ShouldBeInvalid_WhenEmailIsNotValid()
    {
        // Arrange
        var candidate = new CandidateDto
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            Email = "invalid-email",
            CallTimeInterval = "9:00 AM-11:00 AM",
            LinkedInProfileUrl = "https://linkedin.com/in/johndoe",
            GitHubProfileUrl = "https://github.com/johndoe",
            Comment = "Great candidate."
        };

        // Act
        var validationResults = ValidateModel(candidate);

        // Assert
        Assert.IsNotEmpty(validationResults);
        Assert.That(validationResults, Has.One.Matches<ValidationResult>(v => v.MemberNames.Contains("Email")));
    }

    [Test]
    public void CandidateDto_ShouldBeInvalid_WhenUrlsAreNotValid()
    {
        // Arrange
        var candidate = new CandidateDto
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            Email = "john.doe@example.com",
            CallTimeInterval = "9:00 AM-11:00 AM",
            LinkedInProfileUrl = "not-a-valid-url",
            GitHubProfileUrl = "not-a-valid-url",
            Comment = "Great candidate."
        };

        // Act
        var validationResults = ValidateModel(candidate);

        // Assert
        Assert.IsNotEmpty(validationResults);
        Assert.That(validationResults, Has.One.Matches<ValidationResult>(v => v.MemberNames.Contains("LinkedInProfileUrl")));
        Assert.That(validationResults, Has.One.Matches<ValidationResult>(v => v.MemberNames.Contains("GitHubProfileUrl")));
    }


    [Test]
    public void CandidateDto_ShouldBeInvalid_WhenPhoneNumberIsNotValid()
    {
        // Arrange
        var candidate = new CandidateDto
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890__",
            Email = "john.doe@example.com",
            CallTimeInterval = "9:00 AM-11:00 AM",
            LinkedInProfileUrl = "https://linkedin.com/in/johndoe",
            GitHubProfileUrl = "https://github.com/johndoe",
            Comment = "Great candidate."
        };

        // Act
        var validationResults = ValidateModel(candidate);

        // Assert
        Assert.IsNotEmpty(validationResults);
        Assert.That(validationResults, Has.One.Matches<ValidationResult>(v => v.MemberNames.Contains("PhoneNumber")));
    }
}


