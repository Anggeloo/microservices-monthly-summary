using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace microservices_monthly_summary.Models
{
    [Table("monthly_summary")]
    public class MonthlySummary
    {
        [Key]
        [Column("summary_id")]
        public int SummaryId { get; set; }

        [Required]
        [Column("summary_code")]
        [StringLength(100)]
        public string SummaryCode { get; set; }

        [Required]
        [Column("month")]
        [StringLength(50)]
        public string Month { get; set; }

        [Column("total_completed_orders")]
        public int TotalCompletedOrders { get; set; } = 0;

        [Column("total_sales")]
        public decimal TotalSales { get; set; } = 0.00m;

        [Column("best_team")]
        [StringLength(100)]
        public string? BestTeam { get; set; }

        [Column("average_performance")]
        public decimal AveragePerformance { get; set; } = 0.00m;

        [Column("recent_issues")]
        public string? RecentIssues { get; set; }

        [Column("generated_at")]
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        [Column("additional_notes")]
        public string? AdditionalNotes { get; set; }

        [Column("status")]
        public bool Status { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
