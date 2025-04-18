using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace RepositoryTests;


public class TestRepository : BaseRepository<UserEntity>
{
    public TestRepository(AppDbContext context) : base(context) { }
}

public class BaseTests
{
    [Fact]
    public async Task ExistsAsync_Should_ReturnTrue_IfEntityExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_Exists")
            .Options;

        using var context = new AppDbContext(options);

        context.Users.Add(new UserEntity { FirstName = "Daniel" });
        await context.SaveChangesAsync();

        var repo = new TestRepository(context);

        // Act
        var result = await repo.ExistsAsync(x => x.FirstName == "Daniel");

        // Assert
        Assert.True(result.Succeeded);
        Assert.True(result.Data);
    }

}
