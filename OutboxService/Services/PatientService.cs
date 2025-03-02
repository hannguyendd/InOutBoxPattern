using Mapster;
using Microsoft.EntityFrameworkCore;
using OutboxService.Infrastructure;
using Shared.Contracts;

namespace OutboxService.Services;

public class PatientService(OutboxDbContext context)
{
    public async Task<IEnumerable<PatientResponse>> GetAllAsync()
    {
        var patients = await context.Patients.AsNoTracking().ToArrayAsync();

        return patients.Adapt<IEnumerable<PatientResponse>>();
    }
}