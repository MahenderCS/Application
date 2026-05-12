namespace JobPortalApi.Models;

public sealed class CandidateSearchQuery
{
    public string? Skill { get; set; }
    public int? MinimumYearsOfExperience { get; set; }
    public string? PreferredLocation { get; set; }
}
