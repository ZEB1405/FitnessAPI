using System.ComponentModel.DataAnnotations;

namespace FitnessApi.Models;

    /// <summary>
    /// Represents a weight entry in the fitness tracker.
    /// </summary>
    public class WeightEntry
    {
        public int Id { get; set; }
        [Range(0, 1000, ErrorMessage = "Weight must be between 0 and 1000.")]
        public decimal Weight { get; set; }
        public DateTime RecordedAt { get; set; }
        public string? Notes { get; set; }
    }
