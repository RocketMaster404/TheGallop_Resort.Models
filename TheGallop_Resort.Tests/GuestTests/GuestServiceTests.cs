using Microsoft.EntityFrameworkCore;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.Services;

namespace TheGallop_Resort.Tests;

[TestClass]
public class GuestServiceTests
{

    private GaloppDbContext? _ctx;
    private GuestService? _service;

    [TestInitialize]
    public void setup()
    {
        var options = new DbContextOptionsBuilder<GaloppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _ctx = new GaloppDbContext(options);
        _service = new GuestService(_ctx);


    }
      
    [TestMethod]
    public void TestMethod1()
    {
    }
}
