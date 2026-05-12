using JobPortalApi.Data;
using JobPortalApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalApi.Controllers;

[ApiController]
[Route("api/employers")]
public sealed class EmployersController : ControllerBase
{
    private readonly CandidateRepository _repository;

    public EmployersController(CandidateRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("candidates/search")]
    public IActionResult SearchCandidates([FromQuery] CandidateSearchQuery query)
    {
        var candidates = _repository.Search(query);
        return Ok(candidates);
    }
}
