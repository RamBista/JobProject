using JobBackEnd.DAL.Context.Entities;
using JobBackEnd.BLL.Dtos;
namespace JobBackEnd.BLL.Mappers;

public static class CandidateMapper
{
    public static Candidate ToModel(this CandidateDto candidateDto)
    {
        return new Candidate()
        {
            FirstName = candidateDto.FirstName,
            LastName = candidateDto.LastName,
            PhoneNumber = candidateDto.PhoneNumber,
            Email = candidateDto.Email,
            CallTimeInterval = candidateDto.CallTimeInterval ?? string.Empty,
            LinkedInProfileUrl = candidateDto.LinkedInProfileUrl ?? string.Empty,
            GitHubProfileUrl = candidateDto.GitHubProfileUrl ?? string.Empty,
            Comment = candidateDto.Comment,
            CreationDateTime = DateTime.UtcNow.ToString()
        };
    }

}
