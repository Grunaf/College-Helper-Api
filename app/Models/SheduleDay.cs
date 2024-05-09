using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models
{
    public class SheduleDay
    {
        public int Id { get; set; }
        [Column(TypeName = "smallint")]
        public short CountWeek { get; set; }
        public Subject? FirstSubject { get; set; }
        public Subject? SecondSubject { get; set; }
        public Subject? ThirdSubject { get; set; }
        public Subject? FourthSubject { get; set; }
    }
}
