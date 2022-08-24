using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Common.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext , IAppDbContext
    {
        private readonly IMediator _mediator;

        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator):base(options)
        {
            _mediator = mediator;
        }




        public DbSet<Language> Languages => Set<Language>();
        public DbSet<Framework> Frameworks => Set<Framework>(); 
        public DbSet<Course> Courses => Set<Course>();  

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());   
            base.OnModelCreating(builder);  
        }
        public override  async Task<int> SaveChangesAsync(CancellationToken cancellationToken= default)
        {
            await _mediator.DispatchDomainEvents(this);
            return await base.SaveChangesAsync(cancellationToken);  
        }
    }
}
