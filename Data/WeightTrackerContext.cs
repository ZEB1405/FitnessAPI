using FitnessApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessApi.Data;

public class WeightTrackerContext : DbContext
{
    public WeightTrackerContext(DbContextOptions<WeightTrackerContext> options) : base(options)
    {
        
    }
    public DbSet<WeightEntry> WeightEntries { get; set; }
}