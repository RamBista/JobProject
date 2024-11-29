using System.ComponentModel.DataAnnotations;
namespace JobBackEnd.BLL.Dtos;

public class CandidateDto
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = default!;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = default!;

    [Phone]
    [MaxLength(15)]
    public string PhoneNumber { get; set; } = default!;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = default!;

    [MaxLength(100)]
    public string? CallTimeInterval { get; set; } = default!;

    [Url]
    [MaxLength(255)]
    public string? LinkedInProfileUrl { get; set; } = default!;

    [Url]
    [MaxLength(255)]
    public string? GitHubProfileUrl { get; set; } = default!;

    [Required]
    [MaxLength(1000)]
    public string Comment { get; set; } = default!;
}
