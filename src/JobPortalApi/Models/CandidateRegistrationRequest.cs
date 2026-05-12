namespace JobPortalApi.Models;

public sealed class CandidateRegistrationRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Uan { get; set; } = string.Empty;
    public string SkillsCsv { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public string? PreferredLocation { get; set; }
}
