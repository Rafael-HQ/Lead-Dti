using Desafio_Dti_Lead.Application.DTOs;
using Desafio_Dti_Lead.Domian;
using Desafio_Dti_Lead.Domian.Entities;
using Desafio_Dti_Lead.Domian.Enums;

namespace Desafio_Dti_Lead.Application.Services;

public class LeadServices : ILeadServices
{
    private readonly ILeadRepository _leadRepository;

    public LeadServices(ILeadRepository leadRepository)
    {
        _leadRepository = leadRepository;
    }

    public async Task<List<Lead>> GetLeadsInvitedAsync()
    {
        return await _leadRepository.GetLeadsInvitedAsync();
    }

    public async Task<List<Lead>> GetLeadsAcceptedAsync()
    {
        return await _leadRepository.GetLeadsAcceptedAsync();
    }

    public async Task AddLeadAsync(CreateLeadDto request)
    {
        if (request.Price < 0)
            throw new ArgumentException("Preço não pode ser negativo");

        var newLead = new Lead(
            fristName: request.FristName,
            fullName: request.FullName,
            email: request.Email,
            phoneNumber: request.PhoneNumber,
            suburb: request.Suburb,
            category: request.Category,
            description: request.Description,
            price: request.Price
        );
        await _leadRepository.AddLeadAsync(newLead);
    }


    public async Task AcceptLeadAsync(int id)
    {
        var lead = await _leadRepository.GetLeadByIdAsync(id);
        if (lead == null) 
            throw new Exception("Lead não encontrado");
        lead.Accept();
        await _leadRepository.UpdateAsync(lead);
    }

    public async Task DeclineLeadAsync(int id)
    {
        var lead = await _leadRepository.GetLeadByIdAsync(id);
        if (lead == null) 
            throw new Exception("Lead não encontrado");
        lead.Decline();
        await _leadRepository.UpdateAsync(lead);
    }
}