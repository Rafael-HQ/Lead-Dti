using Desafio_Dti_Lead.Domian.Enums;

namespace Desafio_Dti_Lead.Application.DTOs;

public class LeadInvitedDto
{
    public int ID { get; set; }
    public string FristName { get; set; }
    public DateTime DateCreated { get; set; }
    public string Category { get; private set; }
    public string Suburb { get; private set; }
    public decimal Price { get; private set; }
    public LeadStatus Status { get; private set; }
}