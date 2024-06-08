using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models
{
    public class SheduleDaySubject
    {   
        public int SheduleDayId { get; set; }
        public SheduleDay SheduleDay { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string? SubgroupSequence { get; set; }
        public byte Spot { get; set; }
    }
}
