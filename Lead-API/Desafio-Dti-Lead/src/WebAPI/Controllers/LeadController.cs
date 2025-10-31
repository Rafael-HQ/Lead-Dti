using Desafio_Dti_Lead.Application.DTOs;
using Desafio_Dti_Lead.Domian;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Dti_Lead.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeadController : ControllerBase
{
    private readonly ILeadServices _leadServices;
    
    public LeadController(ILeadServices leadServices )
    {
        _leadServices = leadServices;
    }
    
    [HttpGet("invited")]
    public IActionResult GetInvitedLeads()
    {
        var leads = _leadServices.GetLeadsInvitedAsync().Result;
        return Ok(leads);
    }

    [HttpGet("accepted")]
    public IActionResult GetAcceptedLeads()
    {
        var leads = _leadServices.GetLeadsAcceptedAsync().Result;
        return Ok(leads);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateLead([FromBody] CreateLeadDto lead)
    {
        await _leadServices.AddLeadAsync(lead);
        return Ok("Lead foi criado com sucesso");
    }

    [HttpPut("accept/{id}")]
    public async Task<IActionResult> AcceptLead(int id)
    {
        await _leadServices.AcceptLeadAsync(id);
        return Ok("Lead aceito com sucesso");
    }

    [HttpPut("decline/{id}")]
    public async Task<IActionResult> DeclineLead(int id)
    {
        await _leadServices.DeclineLeadAsync(id);
        return Ok("Lead recusado com sucesso");
    }
}