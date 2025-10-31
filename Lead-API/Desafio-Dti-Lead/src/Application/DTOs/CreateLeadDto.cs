using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Dti_Lead.Application.DTOs;

public class CreateLeadDto
{
    public string FristName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Suburb { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}