using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface.WebApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSchedule.Service.Services.WebApi
{
    public class WebApiService : IWebApiService
    {
        public const string baseUrl = @"https://localhost:44390/api";
        public async Task<bool> DeleteEmployee(int id)
        {
            using(var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{baseUrl}/employee/{id}/");
                return response.IsSuccessStatusCode; 
            }
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{baseUrl}/company/");
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var companies = JsonConvert.DeserializeObject<List<Company>>(content);
                return companies;
            }
        }

        public async Task<Schedule> GetScheduleById(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = await client.GetAsync($"{baseUrl}/schedule/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var content = await response.Content.ReadAsStringAsync();
                var companies = JsonConvert.DeserializeObject<Schedule>(content);
                return companies;
            }
        }

        public async Task<bool> UpdateSchedule(Schedule schedule)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Put;
                request.Content = JsonContent.Create(schedule);
                request.RequestUri = new Uri(baseUrl + $"/schedule/{schedule.Id}");
                var response = await client.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
