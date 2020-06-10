using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffRosterManager.Models
{
    public class MockOffRosterRequestRepository 
    {

        public List<OffRosterRequest> GetAllRequests() => new List<OffRosterRequest>()
        {
            new OffRosterRequest {Id = 1, ThreeLetterCode = "ABC", StartDate = DateTime.Now.AddDays(-7), EndDate = DateTime.Now.AddMonths(1).Date, IsOpenEnded = false, OffRosterCode = "ORS"},
            new OffRosterRequest {Id = 2, ThreeLetterCode = "DEF", StartDate = DateTime.Now.AddDays(5).Date, EndDate = DateTime.Now.AddDays(6).Date, IsOpenEnded = false, OffRosterCode = "LVC"},
            new OffRosterRequest {Id = 3, ThreeLetterCode = "GHI", StartDate = DateTime.Now.Date, IsOpenEnded = true, OffRosterCode = "MSG"},
            new OffRosterRequest {Id = 4, ThreeLetterCode = "JKL", StartDate = DateTime.Now.AddDays(-5).Date, EndDate = DateTime.Now.AddDays(-2).Date, IsOpenEnded = false, OffRosterCode = "TOD"},
            new OffRosterRequest {Id = 5, ThreeLetterCode = "MNO", StartDate = DateTime.Now.AddMonths(1).Date, EndDate = DateTime.Now.AddDays(1).AddMonths(1).Date, IsOpenEnded = false, OffRosterCode = "ORD"},
            new OffRosterRequest {Id = 5, ThreeLetterCode = "MNO", StartDate = DateTime.Now.AddMonths(-3).Date, EndDate = DateTime.Now.AddMonths(-3).AddDays(1).AddMonths(1).Date, IsOpenEnded = false, OffRosterCode = "ORD"}

        };


        public List<OffRosterRequest> OpenRequests
        {
            get { return GetAllRequests().Where(n => n.IsOpenEnded == true || (n.EndDate > DateTime.Now.Date && n.StartDate <= DateTime.Now.Date)).ToList(); }
        }

        //async Task<OffRosterRequest> .GetOffRosterRequestById(int Id)
        //{
        //    return GetAllRequests().FirstOrDefault(n => n.Id == Id);
        //}

        public IEnumerable<OffRosterRequest> GetOffRosterBetweenDates(DateTime startDate, DateTime endDate)
        {
            // Find all requests that start before the endDate and finish after the startDate. OR find all open ended requests that start before the endDate.
            return GetAllRequests().Where(n => (n.StartDate.Date <= endDate.Date && n.EndDate.GetValueOrDefault().Date >= startDate.Date)
            || (n.IsOpenEnded && n.StartDate <= endDate));
        }

        public IEnumerable<OffRosterRequest> GetOffRosterRequestByCrewMember(string threeLetterCode)
        {
            return GetAllRequests().Where(n => n.ThreeLetterCode == threeLetterCode);
        }

        // async Task<List<OffRosterRequest>> IOffRosterRequestRepository.GetAllRequests()
        //{
        //    List<OffRosterRequest> requests = new List<OffRosterRequest>()
        //    {
        //    new OffRosterRequest {Id = 1, ThreeLetterCode = "ABC", StartDate = DateTime.Now.AddDays(-7), EndDate = DateTime.Now.AddMonths(1).Date, IsOpenEnded = false, OffRosterCode = "ORS"},
        //    new OffRosterRequest {Id = 2, ThreeLetterCode = "DEF", StartDate = DateTime.Now.AddDays(5).Date, EndDate = DateTime.Now.AddDays(6).Date, IsOpenEnded = false, OffRosterCode = "LVC"},
        //    new OffRosterRequest {Id = 3, ThreeLetterCode = "GHI", StartDate = DateTime.Now.Date, IsOpenEnded = true, OffRosterCode = "MSG"},
        //    new OffRosterRequest {Id = 4, ThreeLetterCode = "JKL", StartDate = DateTime.Now.AddDays(-5).Date, EndDate = DateTime.Now.AddDays(-2).Date, IsOpenEnded = false, OffRosterCode = "TOD"},
        //    new OffRosterRequest {Id = 5, ThreeLetterCode = "MNO", StartDate = DateTime.Now.AddMonths(1).Date, EndDate = DateTime.Now.AddDays(1).AddMonths(1).Date, IsOpenEnded = false, OffRosterCode = "ORD"},
        //    new OffRosterRequest {Id = 5, ThreeLetterCode = "MNO", StartDate = DateTime.Now.AddMonths(-3).Date, EndDate = DateTime.Now.AddMonths(-3).AddDays(1).AddMonths(1).Date, IsOpenEnded = false, OffRosterCode = "ORD"}
        //    };

        //    return requests;
        //}
    }
}
