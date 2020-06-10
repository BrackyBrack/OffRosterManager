using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace OffRosterManager.Models
{
    public class OffRosterRequest : IEquatable<OffRosterRequest>
    {
        public int Id { get; set; }

        private string _threeLetterCode;
        [Display(Name ="Crew members 3 letter code:")]
        [Required(ErrorMessage ="Please enter the crew members 3 letter code")]
        public string ThreeLetterCode { get { return _threeLetterCode; } set { _threeLetterCode = value?.ToUpper(); } }

        [Display(Name ="First date of off roster:")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter a start date for this request")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Last date of off roster:")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Please tick if there is a long term request with no known end date:")]
        public bool IsOpenEnded { get; set; }

        [Display(Name ="Off roster code:")]
        [Required(ErrorMessage = "Please enter an off roster code")]
        public string OffRosterCode { get; set; }
        public bool IsActioned { get; set; }

        public List<OffRosterRequestComment> Comments { get; set; }
        public bool IsActive { get => (IsOpenEnded == false && DateTime.Now > StartDate && DateTime.Now < EndDate.GetValueOrDefault().AddDays(1)); }
        public bool IsFuture { get => DateTime.Now < StartDate; }
        public bool IsHistoric { get => (IsOpenEnded == false && DateTime.Now > EndDate.GetValueOrDefault().AddDays(1)); }

        public OffRosterRequest()
        {

        }
        public bool Equals(OffRosterRequest other)
        {
            return this.Id == other.Id;
        }
    }
}
