using OffRosterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffRosterManager.ViewModels
{
    public class OffRosterListViewModel
    {
        public List<OffRosterRequest> AwaitingRequests { get; set; }
        public List<OffRosterRequest> ActiveRequests { get; set; }
        public List<OffRosterRequest> FutureRequests { get; set; }
        public List<OffRosterRequest> HistoricRequests { get; set; }
        public List<OffRosterRequest> OpenEndedRequests { get; set; }
    }
}
