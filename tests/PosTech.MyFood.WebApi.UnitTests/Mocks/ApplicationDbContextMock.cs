using Microsoft.EntityFrameworkCore;
using PosTech.MyFood.WebApi.Persistence;

namespace PosTech.MyFood.WebApi.UnitTests.Mocks;

public static class ApplicationDbContextMock
{
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);

        return context;
    }
}