using DataBaseService.Db.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataBaseService.Db.Repository;

public class UserRepository(GameDbContext gameDbContext) : DbContext
{
    public void SaveToDb(UserData userData)
    {
        gameDbContext.UserData.Add(userData);
        gameDbContext.SaveChanges();
    }

    public async Task<List<UserData>> GetLastTenRecordsAsync()
    {
        IQueryable<UserData> query = gameDbContext.UserData;

        List<UserData> lastTenRecords = await query
            .OrderByDescending(p => p.Score)
            .Take(10)
            .ToListAsync();

        return lastTenRecords;
    }
}