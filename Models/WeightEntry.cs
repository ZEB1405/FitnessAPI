using System.ComponentModel.DataAnnotations;

namespace FitnessApi.Models;

    /// <summary>
    /// Represents a weight entry in the fitness tracker.
    /// </summary>
    public class WeightEntry
    {
        public int Id { get; set; }
        [Range(1, 600, ErrorMessage = "Weight must be between 1 and 600.")]
        public decimal Weight { get; set; }
        [Required]
        public DateTime? RecordedAt { get; set; }
        public string? Notes { get; set; }
    }
