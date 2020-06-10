using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OffRosterManager.Models
{
    public interface IOffRosterRequestRepository
    {
        Task<List<OffRosterRequest>> GetAllRequests();

        Task<List<OffRosterRequest>> GetRequestsToAction();
        List<OffRosterRequest> OpenRequests { get; }
        Task<OffRosterRequest> GetOffRosterRequestById(int Id);
        Task<List<OffRosterRequest>> GetOffRosterBetweenDates(DateTime startDate, DateTime endDate);

        IEnumerable<OffRosterRequest> GetOffRosterRequestByCrewMember(string threeLetterCode);

        Task Add(OffRosterRequest offRosterRequest);

        void ConfirmRequest(int id);
        Task AddComment(OffRosterRequestComment comment);

        Task Update(OffRosterRequest request);
    }
}