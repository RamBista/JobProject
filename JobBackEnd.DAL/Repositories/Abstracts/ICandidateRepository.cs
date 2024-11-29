using JobBackEnd.DAL.Context.Entities;

namespace JobBackEnd.DAL.Repositories.Abstracts;

public interface ICandidateRepository
{
    Task<Candidate?> GetByEmailAsync(string email);
    Task AddAsync(Candidate candidate);
    Task UpdateAsync(Candidate candidate);
}
