namespace WWW.Models
{
    public class TrainerAvailability
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }

        public DayOfWeek Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public Trainer? Trainer { get; set; }

    }
}
