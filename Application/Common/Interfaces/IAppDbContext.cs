using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAppDbContext
    {
       public DbSet<Language> Languages { get;  }
       public DbSet<Framework> Frameworks { get; }
       public DbSet<Course> Courses { get; }
        DatabaseFacade database { get; }
       Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
