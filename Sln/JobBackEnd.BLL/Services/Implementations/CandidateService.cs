using JobBackEnd.DAL.Context.Entities;
using JobBackEnd.BLL.Dtos;
using JobBackEnd.BLL.Mappers;
using JobBackEnd.DAL.Repositories.Abstracts;
using JobBackEnd.Services.Abstracts;

namespace JobBackEnd.Services.Implementations;
public class CandidateService : ICandidateService
{
    private readonly ICandidateRepository _candidateRepository;

    public CandidateService(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<Candidate> AddOrUpdateAsync(CandidateDto candidateDto)
    {
        var existingCandidate = await _candidateRepository.GetByEmailAsync(candidateDto.Email);

        Candidate candidate;
        if (existingCandidate is null)
        {
            candidate = await AddAsync(candidateDto);
            return candidate;
        }

        candidate = await UpdateAsync(candidateDto, existingCandidate);
        return candidate;
    }

    private async Task<Candidate> AddAsync(CandidateDto candidateDto)
    {
        Candidate candidate = candidateDto.ToModel();
        await _candidateRepository.AddAsync(candidate);
        return candidate;
    }

    private async Task<Candidate> UpdateAsync(CandidateDto candidateDto, Candidate candidate)
    {
        candidate.FirstName = candidateDto.FirstName;
        candidate.LastName = candidateDto.LastName;
        candidate.PhoneNumber = candidateDto.PhoneNumber;
        candidate.CallTimeInterval = candidateDto.CallTimeInterval ?? string.Empty;
        candidate.LinkedInProfileUrl = candidateDto.LinkedInProfileUrl ?? string.Empty;
        candidate.GitHubProfileUrl = candidateDto.GitHubProfileUrl ?? string.Empty;
        candidate.Comment = candidateDto.Comment ?? string.Empty;
        candidate.UpdateDateTime = DateTime.UtcNow.ToString();

        await _candidateRepository.UpdateAsync(candidate);
        return candidate;
    }
}
