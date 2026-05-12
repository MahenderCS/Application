namespace JobPortalApi.Models;

public sealed class CandidateProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Uan { get; set; } = string.Empty;
    public List<string> Skills { get; set; } = new();
    public int YearsOfExperience { get; set; }
    public string? PreferredLocation { get; set; }
    public string ResumeFileName { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
