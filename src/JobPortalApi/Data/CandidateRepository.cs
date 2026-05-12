using System.Collections.Concurrent;
using JobPortalApi.Models;

namespace JobPortalApi.Data;

public sealed class CandidateRepository
{
    private readonly ConcurrentDictionary<Guid, CandidateProfile> _candidates = new();

    public CandidateProfile Add(CandidateProfile profile)
    {
        _candidates[profile.Id] = profile;
        return profile;
    }

    public IEnumerable<CandidateProfile> Search(CandidateSearchQuery query)
    {
        var data = _candidates.Values.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(query.Skill))
        {
            data = data.Where(candidate =>
                candidate.Skills.Any(skill => skill.Equals(query.Skill, StringComparison.OrdinalIgnoreCase)));
        }

        if (query.MinimumYearsOfExperience.HasValue)
        {
            data = data.Where(candidate => candidate.YearsOfExperience >= query.MinimumYearsOfExperience.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.PreferredLocation))
        {
            data = data.Where(candidate =>
                string.Equals(candidate.PreferredLocation, query.PreferredLocation, StringComparison.OrdinalIgnoreCase));
        }

        return data.OrderByDescending(candidate => candidate.CreatedAtUtc).ToArray();
    }

    public CandidateProfile? GetById(Guid id)
    {
        return _candidates.TryGetValue(id, out var profile) ? profile : null;
    }
}
