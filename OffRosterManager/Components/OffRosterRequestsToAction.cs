using Microsoft.AspNetCore.Mvc;
using OffRosterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffRosterManager.Components
{
    public class OffRosterRequestsToAction : ViewComponent
    {
        private readonly IOffRosterRequestRepository _offRosterRequestRepository;

        public OffRosterRequestsToAction(IOffRosterRequestRepository offRosterRequestRepository)
        {
            _offRosterRequestRepository = offRosterRequestRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<OffRosterRequest> allOffRosters = await _offRosterRequestRepository.GetAllRequests();
            List<OffRosterRequest>openOffRosters = allOffRosters.Where(n => n.IsActioned == false).ToList();

            return View(openOffRosters);
        }
    }
}
