using System.ComponentModel.DataAnnotations;
using Desafio_Dti_Lead.Domian.Enums;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Dti_Lead.Domian.Entities;

public class Lead
{
    [Key]
    public int Id { get; private set; }
    
    [Required]
    [MaxLength(20)]
    public string FristName { get; private set; }
    
    [Required]
    [MaxLength(50)]
    public string FullName { get; private set; }
    
    [Required]
    [Phone]
    public string PhoneNumber { get; private set; }
    
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }
    
    [Required]
    public DateTime DateCreated { get; private set; }
    
    [Required]
    [MaxLength(100)]
    public string Suburb { get; private set; }
    
    [Required]
    [MaxLength(50)]
    public string Category { get; private set; }
    
    [MaxLength(150)]
    public string Description { get; private set; }
    
    [Required]
    [Range(0, (double)decimal.MaxValue)]
    [Precision(18, 2)]
    public decimal Price { get; private set; }
    
    public LeadStatus Status { get; private set; }
    

    public Lead(string fristName, string fullName, string email, string phoneNumber, string suburb, string category, string description, decimal price)
    {
        if (price < 0)
            throw new ArgumentException("Price não pode ser negativo");

        FristName = fristName;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        Suburb = suburb;
        Category = category;
        Description = description;
        Price = price;
        DateCreated = DateTime.UtcNow; 
        Status = LeadStatus.Invited;
    }


    public void Accept()
    {
        Status = LeadStatus.Accepted;
        if (Price >= 500)
        {
            Price = Price * 0.9m;
        }
    }

    public void Decline()
    {
        Status = LeadStatus.Declined;
    }
    
}