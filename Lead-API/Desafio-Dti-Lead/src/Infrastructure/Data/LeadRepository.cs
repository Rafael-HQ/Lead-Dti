using Desafio_Dti_Lead.Domian;
using Desafio_Dti_Lead.Domian.Entities;
using Desafio_Dti_Lead.Domian.Enums;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Dti_Lead.Infrastructure;

public class LeadRepository : ILeadRepository
{
    private readonly AppDbContext _context;
    public LeadRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Lead>> GetLeadsInvitedAsync()
    {
        var leads = _context.leads
            .Where(l => l.Status == LeadStatus.Invited)
            .ToListAsync();
        return leads;
    }

    public Task<List<Lead>> GetLeadsAcceptedAsync()
    {
        var leads = _context.leads
            .Where(l => l.Status == LeadStatus.Accepted)
            .ToListAsync();
        return leads;
    }

    public async Task AddLeadAsync(Lead lead)
    {
        _context.leads.Add(lead);
        await  _context.SaveChangesAsync();
    }

    public Task<Lead> GetLeadByIdAsync(int id)
    {
        var lead = _context.leads
            .FirstOrDefaultAsync(l => l.Id == id);
        return lead;
    }
    public async Task UpdateAsync(Lead lead) 
    {
        _context.leads.Update(lead);
        await _context.SaveChangesAsync();
    }
}