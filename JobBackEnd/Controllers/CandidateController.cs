using JobBackEnd.BLL.Dtos;
using JobBackEnd.Constants;
using JobBackEnd.DAL.Context.Entities;
using JobBackEnd.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
namespace JobBackEnd.Controllers;

[ApiController]
[Route("[controller]")]
public class CandidateController : ControllerBase
{
    private ICandidateService _candidateService;

    public CandidateController(ICandidateService candidateService)
    {
        _candidateService = candidateService;
    }

    [HttpGet]
    public List<string> Get()
    {
        return new List<string>() { "value1", "value2" };
    }

    [HttpPost]
    public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateDto candidateDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    Status = 400,
                    Message = ResponseConstants.InvalidInput,
                    Errors = ModelState
                });

            Candidate candidate = await _candidateService.AddOrUpdateAsync(candidateDto);
            return Ok(new
            {
                Status = 200,
                Message = ResponseConstants.Success,
                Data = candidate
            });
        }
        catch (Exception ex)
        {
            string exception = ex.Message;
            // can log exception here

            return StatusCode(500, new
            {
                Status = 500,
                Message = ResponseConstants.ExceptionCaught,
                Details = ResponseConstants.ContactAdmin
            });
        }
    }
}
