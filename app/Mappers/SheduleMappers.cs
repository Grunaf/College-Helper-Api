using app.Dtos.SheduleDay;
using app.Models;

namespace app.Mappers
{
    public static class SheduleMappers
    {
        public static PairDto ToPairDtoFromSheduleDaySubject(this SheduleDaySubject sheduleDaySubject)
        {
            return new PairDto
            {
                Subject = sheduleDaySubject.Subject.Title,
                Subgroup = sheduleDaySubject.Subgroup
            };
        }
    }
}
