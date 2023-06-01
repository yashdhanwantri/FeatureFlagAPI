using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FeatureFlagAPI.Model;

namespace FeatureFlagAPI.Data
{
    public class FeatureFlagAPIContext : DbContext
    {
        public FeatureFlagAPIContext (DbContextOptions<FeatureFlagAPIContext> options)
            : base(options)
        {
        }

        public DbSet<FeatureFlagAPI.Model.FeatureFlag> FeatureFlag { get; set; } = default!;
    }
}
