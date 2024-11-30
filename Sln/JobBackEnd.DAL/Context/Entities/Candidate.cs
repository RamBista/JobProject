namespace JobBackEnd.DAL.Context.Entities;

public class Candidate
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string CallTimeInterval { get; set; } = default!;
    public string LinkedInProfileUrl { get; set; } = default!;
    public string GitHubProfileUrl { get; set; } = default!;
    public string Comment { get; set; } = default!;
    public string CreationDateTime { get; set; } = default!;
    public string? UpdateDateTime { get; set; } = default!;
}
