using JobBackEnd.BLL.Dtos;
using JobBackEnd.DAL.Context.Entities;
using JobBackEnd.DAL.Repositories.Abstracts;
using JobBackEnd.Services.Implementations;
using Moq;

namespace JobBackEnd.BLL.Services.UnitTest;
public class Tests
{
    private readonly Mock<ICandidateRepository> _repositoryMock;
    private readonly CandidateService _service;

    public Tests()
    {
        _repositoryMock = new Mock<ICandidateRepository>();
        _service = new CandidateService(_repositoryMock.Object);
    }

    [Test]
    public async Task AddCandidateAsync_ShouldCreateNewCandidate_WhenEmailDoesNotExist()
    {
        // Arrange
        var candidateDto = new CandidateDto
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            Email = "john.doe1@example.com",
            CallTimeInterval = "9:00 AM-11:00 AM",
            LinkedInProfileUrl = "https://linkedin.com/in/johndoe",
            GitHubProfileUrl = "https://github.com/johndoe",
            Comment = "Great candidate."
        };

        var currentDateTime = DateTime.UtcNow.ToString();

        _repositoryMock.Setup(repo => repo.GetByEmailAsync(candidateDto.Email))
        .ReturnsAsync((Candidate)null); // Candidate does not exist

        _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Candidate>()))
            .Returns(Task.CompletedTask);

        // Act
        var candidate = await _service.AddOrUpdateAsync(candidateDto);

        // Assert
        Assert.NotNull(candidate.CreationDateTime);
        Assert.Null(candidate.UpdateDateTime);
        Assert.True(DateTime.Compare(DateTime.Parse(candidate.CreationDateTime), DateTime.Parse(currentDateTime)) == 0);
        Assert.AreEqual(candidateDto.Email, candidate.Email);

        _repositoryMock.Verify(repo => repo.GetByEmailAsync(candidateDto.Email), Times.Once);
        _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Candidate>()), Times.Once);
    }

    [Test]
    public async Task UpdateCandidateAsync_ShouldUpdateCandidate_WhenEmailExists()
    {
        // Arrange
        var existingCandidate = new Candidate
        {
            Id = 1,
            FirstName = "Jane",
            LastName = "Doe",
            PhoneNumber = "0987654321",
            Email = "jane.doe@example.com",
            CallTimeInterval = "9-11 AM",
            LinkedInProfileUrl = "https://linkedin.com/in/janedoe",
            GitHubProfileUrl = "https://github.com/janedoe",
            Comment = "Needs follow-up.",
            CreationDateTime = DateTime.UtcNow.AddDays(-10).ToString(),
        };

        var updatedCandidateDto = new CandidateDto
        {
            FirstName = "Jane Updated",
            LastName = "Doe Updated",
            PhoneNumber = "1122334455",
            Email = "jane.doe@example.com",
            CallTimeInterval = "1-3 PM",
            LinkedInProfileUrl = "https://linkedin.com/in/janeupdated",
            GitHubProfileUrl = "https://github.com/janeupdated",
            Comment = "Updated follow-up."
        };

        _repositoryMock.Setup(repo => repo.GetByEmailAsync(updatedCandidateDto.Email))
        .ReturnsAsync(existingCandidate); // Candidate exists

        _repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Candidate>()))
            .Returns(Task.CompletedTask);

        // Act

        // update existing candidate
        Candidate candidate = await _service.AddOrUpdateAsync(updatedCandidateDto);

        // Assert
        Assert.NotNull(candidate.UpdateDateTime);
        Assert.AreEqual(existingCandidate.CreationDateTime, candidate.CreationDateTime);
        Assert.True(DateTime.Compare(DateTime.Parse(candidate.UpdateDateTime), DateTime.Parse(existingCandidate.CreationDateTime)) > 0);
        Assert.AreEqual(updatedCandidateDto.Email, candidate.Email);

        _repositoryMock.Verify(repo => repo.GetByEmailAsync(updatedCandidateDto.Email), Times.Once);
        _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Candidate>()), Times.Once);
    }
}