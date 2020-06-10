using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OffRosterManager.Models;

namespace OffRosterManager.Controllers
{
    public class CrewingController : Controller
    {
        private readonly IOffRosterRequestRepository _offRosterRequestRepository;

        public CrewingController(IOffRosterRequestRepository offRosterRequestRepository)
        {
            _offRosterRequestRepository = offRosterRequestRepository;
        }
        public IActionResult RequestsToAction()
        {
            List<OffRosterRequest> openRequests = _offRosterRequestRepository.GetRequestsToAction().Result;

            return View(openRequests);
        }

        public async Task<IActionResult> ConfirmRequest(int id)
        {
            _offRosterRequestRepository.ConfirmRequest(id);
            await Task.Delay(250);
            return RedirectToAction("RequestsToAction");
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(OffRosterRequestComment offRosterRequestComment)
        {
            await _offRosterRequestRepository.AddComment(offRosterRequestComment);
            await Task.Delay(250);

            return RedirectToAction("RequestsToAction");
        }
    }
}