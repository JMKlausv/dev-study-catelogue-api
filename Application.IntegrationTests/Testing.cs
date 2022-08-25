
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace Application.IntegrationTests
{
    [SetUpFixture]
    public partial class Testing
    {
        private static  IConfiguration _configuration;
        private  static  Checkpoint _checkpoint;
        private static  CustomeWebApplicationFactory _factory;
        private static  IServiceScopeFactory _scopeFactory;

        [OneTimeSetUp]  
        public void RunBeforAnyTest()
        {
            _factory = new CustomeWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
            _configuration = _factory.Services.GetRequiredService<IConfiguration>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
            };
        }

        public static async Task addAsync<TEntity>(TEntity entity) 
            where  TEntity: class
        {
           var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public static async Task<TResponse> sendAsync<TResponse>(IRequest<TResponse> request)
        {
            var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
          return await  mediator.Send(request);
        }

        public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }


        public static async Task ResetState()
        {
            await _checkpoint.Reset(_configuration.GetConnectionString("appDbConnectionString"));

        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }

    }
}
