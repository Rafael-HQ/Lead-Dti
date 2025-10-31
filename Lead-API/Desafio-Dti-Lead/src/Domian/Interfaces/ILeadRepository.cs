using Desafio_Dti_Lead.Domian.Entities;

namespace Desafio_Dti_Lead.Domian;

public interface ILeadRepository
{
    Task<List<Lead>> GetLeadsInvitedAsync();
    Task<List<Lead>> GetLeadsAcceptedAsync();
    Task AddLeadAsync(Lead lead);
    Task<Lead> GetLeadByIdAsync(int id);
    Task UpdateAsync(Lead lead);
}