using System.ComponentModel.DataAnnotations;

namespace app.Dtos.StudentAbsence
{
    public class StudentAbsenceDto
    {
        public int? Id { get; set; }
        public long StudentId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Patronymic { get; set; }
        public bool OnPair { get; set; }
    }
}
