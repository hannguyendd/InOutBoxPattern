using System.Net;
using DotNetCore.CAP;
using InboxService.Infrastructure;
using InboxService.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Contracts;

namespace InboxService.Services;

public class UserService(InboxDbContext context, ICapPublisher publisher)
{
    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        var users = await context.Users.AsNoTracking().ToArrayAsync();

        return users.Adapt<IEnumerable<UserResponse>>();
    }

    public async Task<UserResponse> GetAsync(Guid userId)
    {
        var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId) ??
            throw new ApiException(HttpStatusCode.NotFound, "User not found.");

        return user.Adapt<UserResponse>();
    }

    public async Task<UserResponse> AddAsync(SaveUserRequest request)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var user = request.Adapt<User>();
            var messageEvent = user.Adapt<UpsertPatientRequest>();

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            await publisher.PublishAsync(EventKey.Patient.Upsert, messageEvent);
            await transaction.CommitAsync();

            return user.Adapt<UserResponse>();
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync();
            throw new ApiException(HttpStatusCode.BadRequest, ex.Message);
        }
    }

    public async Task<UserResponse> UpdateAsync(Guid userId, SaveUserRequest request)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var user = await context.Users.FindAsync(userId) ??
            throw new ApiException(HttpStatusCode.NotFound, "User not found.");

            user.Name = request.Name;
            user.Email = request.Email;
            user.DoB = request.DoB;

            var messageEvent = user.Adapt<UpsertPatientRequest>();

            await publisher.PublishAsync(EventKey.Patient.Upsert, messageEvent);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();

            return user.Adapt<UserResponse>();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new ApiException(HttpStatusCode.BadRequest, ex.Message);
        }
    }
}