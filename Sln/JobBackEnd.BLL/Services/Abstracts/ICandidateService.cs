using JobBackEnd.DAL.Context.Entities;
using JobBackEnd.BLL.Dtos;

namespace JobBackEnd.Services.Abstracts;

public interface ICandidateService
{
    Task<Candidate> AddOrUpdateAsync(CandidateDto candidateDto);
}
