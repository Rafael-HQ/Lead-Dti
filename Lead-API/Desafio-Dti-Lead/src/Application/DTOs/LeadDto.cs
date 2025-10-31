namespace Desafio_Dti_Lead.Application.DTOs;

public class LeadDto
{
    public int Id { get; set; }
    public string FristName { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime DateCreated { get; set; }
    public string Suburb { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; }
}