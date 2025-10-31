using Desafio_Dti_Lead.Application.DTOs;
using Desafio_Dti_Lead.Application.Services;
using Desafio_Dti_Lead.Domian;
using Desafio_Dti_Lead.Domian.Entities;
using Desafio_Dti_Lead.Domian.Enums;
using Moq;

namespace Desafio_Dti_Lead_Tests;

public class LeadServicesTests
{
    private readonly Mock<ILeadRepository> _mockLeadRepository;
    private readonly LeadServices _leadServices;

    public LeadServicesTests()
    {
        _mockLeadRepository = new Mock<ILeadRepository>();
        _leadServices = new LeadServices(_mockLeadRepository.Object);
    }

    [Fact]
    public async Task GetLeadsInvitedAsync_DeveRetornarListaDeLeads()
    {
        var expectedLeads = new List<Lead>
        {
            new Lead("John", "John Doe", "john@email.com", "123456789", "Suburb1",
                "Painting", "Description1", 100.0m),
            new Lead("Jane", "Jane Smith", "jane@email.com", "987654321", "Suburb2",
                "Plumbing", "Description2", 200.0m)
        };

        _mockLeadRepository
            .Setup(repo => repo.GetLeadsInvitedAsync())
            .ReturnsAsync(expectedLeads);
        
        var result = await _leadServices.GetLeadsInvitedAsync();
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("John", result[0].FristName);
        Assert.Equal("Jane", result[1].FristName);
        _mockLeadRepository.Verify(repo => repo.GetLeadsInvitedAsync(), Times.Once);
    }

    [Fact]
    public async Task GetLeadsAcceptedAsync_DeveRetornarListaDeLeads()
    {
        var expectedLeads = new List<Lead>
        {
            new Lead("Bob", "Bob Brown", "bob@email.com", "555555555", "Suburb3",
                "Electrical", "Description3", 300.0m)
        };

        _mockLeadRepository
            .Setup(repo => repo.GetLeadsAcceptedAsync())
            .ReturnsAsync(expectedLeads);
        
        var result = await _leadServices.GetLeadsAcceptedAsync();
        
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Bob", result[0].FristName);
        _mockLeadRepository.Verify(repo => repo.GetLeadsAcceptedAsync(), Times.Once);
    }

    [Fact]
    public async Task AddLeadAsync_DeveCriarNovoLead()
    {
        var createLeadDto = new CreateLeadDto
        {
            FristName = "Alice",
            FullName = "Alice Johnson",
            Email = "alice@email.com",
            PhoneNumber = "111111111",
            Suburb = "Suburb4",
            Category = "Carpentry",
            Description = "New lead description",
            Price = 150.0m
        };

        _mockLeadRepository
            .Setup(repo => repo.AddLeadAsync(It.IsAny<Lead>()))
            .Returns(Task.CompletedTask);
        
        await _leadServices.AddLeadAsync(createLeadDto);
        
        _mockLeadRepository.Verify(repo => repo.AddLeadAsync(It.Is<Lead>(lead =>
            lead.FristName == "Alice" &&
            lead.FullName == "Alice Johnson" &&
            lead.Email == "alice@email.com" &&
            lead.PhoneNumber == "111111111" &&
            lead.Suburb == "Suburb4" &&
            lead.Category == "Carpentry" &&
            lead.Description == "New lead description" &&
            lead.Price == 150.0m &&
            lead.Status == LeadStatus.Invited
        )), Times.Once);
    }

    [Fact]
    public async Task AcceptLeadAsync_ComIdValido_DeveAceitarLead()
    {
        var leadId = 1;
        var existingLead = new Lead("Test", "Test User", "test@email.com", "999999999",
            "TestSuburb", "Painting", "Test Description", 100.0m);

        _mockLeadRepository
            .Setup(repo => repo.GetLeadByIdAsync(leadId))
            .ReturnsAsync(existingLead);

        _mockLeadRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<Lead>()))
            .Returns(Task.CompletedTask);
        
        await _leadServices.AcceptLeadAsync(leadId);
        
        Assert.Equal(LeadStatus.Accepted, existingLead.Status);
        _mockLeadRepository.Verify(repo => repo.GetLeadByIdAsync(leadId), Times.Once);
        _mockLeadRepository.Verify(repo => repo.UpdateAsync(existingLead), Times.Once);
    }

    [Fact]
    public async Task AcceptLeadAsync_ComIdInvalido_DeveLancarExcecao()
    {
        var invalidId = 999;
        _mockLeadRepository
            .Setup(repo => repo.GetLeadByIdAsync(invalidId))
            .ReturnsAsync((Lead)null);
        
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _leadServices.AcceptLeadAsync(invalidId));

        Assert.Equal("Lead não encontrado", exception.Message);
        _mockLeadRepository.Verify(repo => repo.GetLeadByIdAsync(invalidId), Times.Once);
        _mockLeadRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Lead>()), Times.Never);
    }

    [Fact]
    public async Task DeclineLeadAsync_ComIdValido_DeveRecusarLead()
    {
        var leadId = 1;
        var existingLead = new Lead("Test", "Test User", "test@email.com", "999999999",
            "TestSuburb", "Painting", "Test Description", 100.0m);

        _mockLeadRepository
            .Setup(repo => repo.GetLeadByIdAsync(leadId))
            .ReturnsAsync(existingLead);

        _mockLeadRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<Lead>()))
            .Returns(Task.CompletedTask);
        
        await _leadServices.DeclineLeadAsync(leadId);
        
        Assert.Equal(LeadStatus.Declined, existingLead.Status);
        _mockLeadRepository.Verify(repo => repo.GetLeadByIdAsync(leadId), Times.Once);
        _mockLeadRepository.Verify(repo => repo.UpdateAsync(existingLead), Times.Once);
    }

    [Fact]
    public async Task DeclineLeadAsync_ComIdInvalido_DeveLancarExcecao()
    {
        var invalidId = 999;
        _mockLeadRepository
            .Setup(repo => repo.GetLeadByIdAsync(invalidId))
            .ReturnsAsync((Lead)null);
        
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _leadServices.DeclineLeadAsync(invalidId));

        Assert.Equal("Lead não encontrado", exception.Message);
        _mockLeadRepository.Verify(repo => repo.GetLeadByIdAsync(invalidId), Times.Once);
        _mockLeadRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Lead>()), Times.Never);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    public async Task AcceptLeadAsync_ComDiferentesIdsValidos_DeveFuncionarCorretamente(int leadId)
    {
        var existingLead = new Lead($"User{leadId}", $"User {leadId}", $"user{leadId}@email.com",
            "123456789", "Suburb", "Painting", "Description", 100.0m);

        _mockLeadRepository
            .Setup(repo => repo.GetLeadByIdAsync(leadId))
            .ReturnsAsync(existingLead);

        _mockLeadRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<Lead>()))
            .Returns(Task.CompletedTask);
        
        await _leadServices.AcceptLeadAsync(leadId);
        
        Assert.Equal(LeadStatus.Accepted, existingLead.Status);
        _mockLeadRepository.Verify(repo => repo.GetLeadByIdAsync(leadId), Times.Once);
    }
}