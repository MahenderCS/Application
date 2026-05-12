using JobPortalApi.Data;
using JobPortalApi.Models;
using JobPortalApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalApi.Controllers;

[ApiController]
[Route("api/employees")]
public sealed class EmployeesController : ControllerBase
{
    private readonly CandidateRepository _repository;
    private readonly ResumeStorageService _resumeStorageService;
    private readonly UanValidator _uanValidator;

    public EmployeesController(
        CandidateRepository repository,
        ResumeStorageService resumeStorageService,
        UanValidator uanValidator)
    {
        _repository = repository;
        _resumeStorageService = resumeStorageService;
        _uanValidator = uanValidator;
    }

    [HttpPost("register")]
    [RequestSizeLimit(10 * 1024 * 1024)]
    public async Task<IActionResult> Register(
        [FromForm] CandidateRegistrationRequest request,
        [FromForm] IFormFile resume,
        CancellationToken cancellationToken)
    {
        if (!_uanValidator.IsValid(request.Uan))
        {
            return BadRequest(new { message = "Invalid UAN. It must be exactly 12 digits." });
        }

        if (resume is null || resume.Length == 0)
        {
            return BadRequest(new { message = "Resume file is required." });
        }

        var storedFileName = await _resumeStorageService.SaveAsync(resume, cancellationToken);

        var profile = new CandidateProfile
        {
            FullName = request.FullName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Uan = request.Uan,
            Skills = request.SkillsCsv
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList(),
            YearsOfExperience = request.YearsOfExperience,
            PreferredLocation = request.PreferredLocation,
            ResumeFileName = storedFileName
        };

        _repository.Add(profile);

        return CreatedAtAction(nameof(GetProfile), new { id = profile.Id }, profile);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetProfile(Guid id)
    {
        var profile = _repository.GetById(id);
        return profile is null ? NotFound() : Ok(profile);
    }
}
