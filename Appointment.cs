using System.ComponentModel.DataAnnotations.Schema;

namespace WWW.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public int GymId { get; set; }
        public string MemberId { get; set; } = null!;   // ApplicationUser Id
        public int TrainerId { get; set; }
        public int ServiceId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Gym? Gym { get; set; }
       // public ApplicationUser? Member { get; set; }
        public Trainer? Trainer { get; set; }
        public Service? Service { get; set; }
    }

    public enum AppointmentStatus
    {
        Pending = 0,
        Approved = 1,
        Cancelled = 2
    }
}

