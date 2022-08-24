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
    public class CourseConfigurations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
           builder.Property(c=>c.Title)
                .IsRequired();
           builder.Property(c => c.ContentLink)
                .IsRequired();
           builder.Property(c => c.FrameworkId)
                .IsRequired();
            builder.Property(c => c.Difficulty)
                .IsRequired();
            builder.Property(c => c.PlatformType)
                .IsRequired();
            builder.Property(c => c.UploadedBy)
                .IsRequired();
            builder.Property(c => c.Division)
                .IsRequired();
       



        }
    }
}
