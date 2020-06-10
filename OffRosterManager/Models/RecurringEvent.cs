using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffRosterManager.Models
{
    public class RecurringEvent
    {
        public int RecurringEventId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsOpenEnded { get; set; }
        public int Frequency { get; set; }
        public bool IsWeekly { get; set; }
        public bool IsMonthly { get; set; }
        public DayOfTheWeek[] DayOfTheWeek { get; set; }
        public PositionInMonth PositionInMonth { get; set; }
    }
}
