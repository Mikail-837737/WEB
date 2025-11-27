using System.ComponentModel.DataAnnotations.Schema;

namespace WWW.Models
{
    public class Service
    {
        public int Id { get; set; }
        public int GymId { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int DurationMinutes { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;

        public Gym? Gym { get; set; }
        public ICollection<TrainerService> TrainerServices { get; set; } = new List<TrainerService>();
        public ICollection<TrainerAvailability> Availabilities { get; set; } = new List<TrainerAvailability>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
