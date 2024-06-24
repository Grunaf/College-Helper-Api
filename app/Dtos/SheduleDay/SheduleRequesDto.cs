namespace app.Dtos.SheduleDay
{
    public class SheduleRequestDto
    {
        private const int DefaultWeekCount = 2;
        public WeekDto[] Weeks { get; set; } = new WeekDto[DefaultWeekCount];
    }

    public class WeekDto
    {
        public List<DayDto> Days { get; set; } = [];
    }

    public class DayDto
    {
        public List<PairDto?> PairsInDay { get; set; } = [];
    }

    public class PairDto
    {
        public string Subject { get; set; }
        public char? Subgroup { get; set; }
    }
}
