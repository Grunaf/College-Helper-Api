namespace app.Dtos.SheduleDay
{
    public class CreateSheduleRequestDto
    {
        public WeekDto[] Weeks { get; set; } = new WeekDto[2];
    }

    public class WeekDto
    {
        public List<DayDto> Days { get; set; }
    }

    public class DayDto
    {
        public string PairsInDay { get; set; }/*
        public string SubgroupSequence { get; set; }*/
    }
}
