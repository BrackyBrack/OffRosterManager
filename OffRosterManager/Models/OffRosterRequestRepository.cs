using System.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Globalization;

namespace OffRosterManager.Models
{
    public class OffRosterRequestRepository : IOffRosterRequestRepository
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private Uri BaseEndPoint { get; set; }

        public async Task<List<OffRosterRequest>> GetAllRequests()
        {
            var response = await _httpClient.GetAsync("http://localhost:65105/api/Manager", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<OffRosterRequest>>(data);
        }

        public async Task<List<OffRosterRequest>> GetRequestsToAction()
        {
            var response = await _httpClient.GetAsync("http://localhost:65105/api/Manager/ToAction", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<OffRosterRequest>>(data);
        }

        public List<OffRosterRequest> OpenRequests => throw new NotImplementedException();

        public async Task<List<OffRosterRequest>> GetOffRosterBetweenDates(DateTime startDate, DateTime endDate)
        {
            var response = await _httpClient.GetAsync("http://localhost:65105/api/Manager/BetweenDates?start=" + startDate.Date.ToShortDateString() +"&end=" + endDate.Date.ToShortDateString(), HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<OffRosterRequest>>(data);
        }

        public IEnumerable<OffRosterRequest> GetOffRosterRequestByCrewMember(string threeLetterCode)
        {
            throw new NotImplementedException();
        }

        public async Task<OffRosterRequest> GetOffRosterRequestById(int Id)
        {
            var response = await _httpClient.GetAsync("http://localhost:65105/api/Manager" +"/"+Id, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OffRosterRequest>(data);
        }

        public async Task Add(OffRosterRequest offRosterRequest)
        {
            var offRosterToPost = new
            {                
                Id = 0,
                offRosterRequest.ThreeLetterCode,
                StartDate = offRosterRequest.StartDate.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
                EndDate = offRosterRequest.EndDate.GetValueOrDefault().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
                offRosterRequest.IsOpenEnded,
                offRosterRequest.OffRosterCode,
                RequestingManager = "Dave",
                IsActioned = false
            };

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-wwww-form-urlencoded"));

            await _httpClient.PostAsJsonAsync("http://localhost:65105/api/Manager/", offRosterToPost);
        }

        public void ConfirmRequest(int id)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-wwww-form-urlencoded"));
            _httpClient.PutAsJsonAsync("http://localhost:65105/api/Manager/" + id, true);
        }

        public async Task AddComment(OffRosterRequestComment comment)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            await _httpClient.PostAsJsonAsync("http://localhost:65105/api/Comment/", comment);
        }

        public async Task Update(OffRosterRequest request)
        {
            var initialRequest = GetOffRosterRequestById(request.Id).Result;

            StringBuilder sb = new StringBuilder();
            if(initialRequest.StartDate != request.StartDate)
            {
                sb.Append($"<p>Start Date changed from {initialRequest.StartDate.ToShortDateString()} to {request.StartDate.ToShortDateString()}</p>");
            }
            if(initialRequest.EndDate != request.EndDate)
            {
                sb.Append($"<p>End Date changed from {initialRequest.EndDate.Value.ToShortDateString()} to {request.EndDate.Value.ToShortDateString()}</p>");
            }
            if(initialRequest.OffRosterCode != request.OffRosterCode)
            {
                sb.Append($"<p>Off Roster code changed from {initialRequest.OffRosterCode} to {request.OffRosterCode}</p>");
            }
        }

    }
}
