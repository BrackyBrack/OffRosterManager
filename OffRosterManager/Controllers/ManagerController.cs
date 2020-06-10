using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OffRosterManager.Models;
using OffRosterManager.ViewModels;

namespace OffRosterManager.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IOffRosterRequestRepository _offRosterRequestRepository;

        public ManagerController(IOffRosterRequestRepository offRosterRequestRepository)
        {
            _offRosterRequestRepository = offRosterRequestRepository;
        }
        public IActionResult NewRequest()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmNewRequest(OffRosterRequest request)
        {
            if (ModelState.IsValid)
            {
                _offRosterRequestRepository.Add(request);
                return View(request);
            }
            else return View("NewRequest");
        }

        public IActionResult ConfirmAmendRequest(OffRosterRequest request)
        {
            //if (ModelState.IsValid)
            //{
                _offRosterRequestRepository.Update(request);
                return View(request);
            //}
            //// Change this in case of error!
            //else return View("AmendRequest");
        }

        public IActionResult ViewAllRequests()
        {
            List<OffRosterRequest> allRequests = _offRosterRequestRepository.GetOffRosterBetweenDates(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(2)).Result;
            List<OffRosterRequest> awaitingRequests = _offRosterRequestRepository.GetRequestsToAction().Result;

            foreach (var request in awaitingRequests)
            {
                if(allRequests.Contains(request) == false)
                {
                    allRequests.Add(request);
                }
            }


            OffRosterListViewModel offRosterListViewModel = new OffRosterListViewModel
            {
                AwaitingRequests = allRequests.Where(n => n.IsActioned == false).ToList(),
                ActiveRequests = allRequests.Where(n => n.IsActive && n.IsActioned).ToList(),
                OpenEndedRequests = allRequests.Where(n => n.IsOpenEnded && n.IsActioned).ToList(),
                FutureRequests = allRequests.Where(n => n.IsFuture && n.IsActioned).ToList(),
                HistoricRequests = allRequests.Where(n => n.IsHistoric && n.IsActioned).OrderBy(n => n.StartDate).ToList()
            };

            return View(offRosterListViewModel);
        }

        public IActionResult AmendRequest(int requestId)
        {
            OffRosterRequest request = _offRosterRequestRepository.GetOffRosterRequestById(requestId).Result;

            return View(request);
        }
    }
}