using JobBackEnd.DAL.Context;
using JobBackEnd.DAL.Context.Entities;
using JobBackEnd.DAL.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace JobBackEnd.Repositories.Implementations;

public class SQLCandidateRepository : ICandidateRepository
{
    private readonly JobDbContext _context;

    public SQLCandidateRepository(JobDbContext context)
    {
        _context = context;
    }

    public async Task<Candidate?> GetByEmailAsync(string email)
    {
        var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
        return candidate;
    }

    public async Task AddAsync(Candidate candidate)
    {
        _context.Candidates.Add(candidate);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Candidate candidate)
    {
        _context.Candidates.Update(candidate);
        await _context.SaveChangesAsync();
    }
}
