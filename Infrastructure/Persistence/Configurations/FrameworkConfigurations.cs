using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class FrameworkConfigurations : IEntityTypeConfiguration<Framework>
    {
        public void Configure(EntityTypeBuilder<Framework> builder)
        {
            builder.Property(f => f.Name)
                .IsRequired();
            builder.Property(f=>f.Type)
                .IsRequired();   
            builder.Property(f=>f.LanguageId)
                .IsRequired(); 
        }
    }
}
