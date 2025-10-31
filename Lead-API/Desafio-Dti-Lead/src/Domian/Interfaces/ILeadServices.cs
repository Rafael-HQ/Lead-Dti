using Desafio_Dti_Lead.Application.DTOs;
using Desafio_Dti_Lead.Domian.Entities;

namespace Desafio_Dti_Lead.Domian;

public interface ILeadServices
{
    public Task<List<Lead>> GetLeadsInvitedAsync();
    public Task<List<Lead>> GetLeadsAcceptedAsync();
    public Task AddLeadAsync(CreateLeadDto request);
    public Task AcceptLeadAsync(int id);
    public Task DeclineLeadAsync(int id);
}