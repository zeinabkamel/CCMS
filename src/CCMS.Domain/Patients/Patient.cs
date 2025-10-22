using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
 
namespace CCMS.Patients;

public class Patient : AggregateRoot<Guid>
{
    public string Code { get; private set; }
    public string FullName { get; private set; }
    public string Phone { get; private set; }
    public string? Gender { get; private set; } // optional: code or enum
    public DateTime? Dob { get; private set; }
    public string? Notes { get; private set; }

    protected Patient() { }

    public Patient(Guid id, string code, string fullName, string phone) : base(id)
    {
        Code = Check.NotNullOrWhiteSpace(code, nameof(code), maxLength: 32);
        FullName = Check.NotNullOrWhiteSpace(fullName, nameof(fullName), maxLength: 128);
        Phone = Check.NotNullOrWhiteSpace(phone, nameof(phone), maxLength: 32);
    }

    public void Update(string fullName, string phone, string? gender = null, DateTime? dob = null, string? notes = null)
    {
        FullName = Check.NotNullOrWhiteSpace(fullName, nameof(fullName), maxLength: 128);
        Phone = Check.NotNullOrWhiteSpace(phone, nameof(phone), maxLength: 32);
        Gender = gender;
        Dob = dob;
        Notes = notes;
    }
}
