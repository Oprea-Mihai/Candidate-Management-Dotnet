using CandidateManagement.Repository.Entities;
using CandidateManagement.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CandidateManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost("AddOrUpdateCandidate")]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _candidateService.AddOrUpdateCandidateAsync(candidate);
            return Ok();
        }

    }
}
