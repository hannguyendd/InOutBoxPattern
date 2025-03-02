namespace Shared.Contracts;

public record UpsertPatientRequest(Guid? Id, string Name, string Email, DateTime DoB);

public record PatientResponse(Guid Id, string Name, string Email, DateTime DoB, string Address, string Phone);