namespace WWW.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public int GymId { get; set; }

        public string FullName { get; set; } = null!;
        public string? Specializations { get; set; }
        public string? Bio { get; set; }

        public Gym? Gym { get; set; }
        public ICollection<TrainerService> TrainerServices { get; set; } = new List<TrainerService>();
        public ICollection<TrainerAvailability> Availabilities { get; set; } = new List<TrainerAvailability>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
