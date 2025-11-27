namespace WWW.Models
{
    public class Gym
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public TimeOnly? OpeningTime { get; set; }
        public TimeOnly? ClosingTime { get; set; }
        public ICollection<Service> Services { get; set; } = new List<Service>();
        public ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
