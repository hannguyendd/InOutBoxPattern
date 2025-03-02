using DotNetCore.CAP;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OutboxService.Infrastructure;
using OutboxService.Models;
using Shared.Constants;
using Shared.Contracts;

namespace OutboxService.Services;

public class PatientEventConsumer(OutboxDbContext dbContext) : ICapSubscribe
{
    private readonly OutboxDbContext dbContext = dbContext;

    [CapSubscribe(EventKey.Patient.Upsert)]
    public async Task UpsertAsync(UpsertPatientRequest request)
    {
        var patient = request.Id != null ? await dbContext.Patients.FirstOrDefaultAsync(p => p.Id == request.Id) : null;

        if (patient == null)
        {
            patient = request.Adapt<Patient>();
            await dbContext.Patients.AddAsync(patient);
        }
        else
        {
            patient.Name = request.Name;
            patient.Email = request.Email;
            patient.DoB = request.DoB;
        }

        await dbContext.SaveChangesAsync();
    }
}