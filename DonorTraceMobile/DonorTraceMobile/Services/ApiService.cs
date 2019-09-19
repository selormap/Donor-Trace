using DonorTraceMobile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DonorTraceMobile.Services
{
    public class ApiService
    {
       // string apiUrl = "https://dtrace.azurewebsites.net/api/";
        string apiUrl = "http://10.0.2.2:5000/api/";
        public async Task<HttpResponseMessage> RegisterUser(string email, string password, string confirmPassword)
        {
            var registerModel = new RegisterModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(apiUrl + "account/register", content);
            return response;
;        }

        public async Task<bool> Login(string email, string password)
        {
            var loginModel = new LoginModel()
            {
                Email = email,
                Password = password,

            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(apiUrl + "account/login", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<TokenResponse>(jsonResult);
                var accessToken = token;
                Settings.Token = accessToken.Token;
                Settings.Id = token.Id;
                Settings.Email = email;
                Settings.Password = password;
                Settings.Role = accessToken.Role;
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<List<OrganList>> OrganList()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(apiUrl + "repo/organs");
            return JsonConvert.DeserializeObject<List<OrganList>>(json);
        }

        public async Task<List<RegionModel>> GetRegions()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(apiUrl + "repo/regions");
            return JsonConvert.DeserializeObject<List<RegionModel>>(json);
        }

        public async Task<List<BloodGroup>> GetBloodGroups()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(apiUrl + "repo/bloodgroups");
            return JsonConvert.DeserializeObject<List<BloodGroup>>(json);
        }

        public async Task<List<BloodType>> GetBloodType(int id)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(apiUrl + "repo/bloodtype/" + id);
            return JsonConvert.DeserializeObject<List<BloodType>>(json);
        }

        public async Task<List<BloodType>> GetBloodTypes()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(apiUrl + "repo/blood-type");
            return JsonConvert.DeserializeObject<List<BloodType>>(json);
        }

        public async Task<bool> BecomeADonor(Donor donor)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(donor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(apiUrl + "repo/donate", content);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.Token);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddOrganOption(OrganOption option)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(option);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(apiUrl + "repo/organ-option", content);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.Token);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddBloodOrganOption(BloodOrganOption option)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(option);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(apiUrl + "repo/organ-option", content);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.Token);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<DonorModel>> GetDonors()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.Token);
            var response = await httpClient.GetStringAsync(apiUrl + "repo/donors");
            return JsonConvert.DeserializeObject<List<DonorModel>>(response);
        }

        public async Task<DonorModel> GetDonor(string id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.Token);
            var response = await httpClient.GetStringAsync(apiUrl + "repo/donors/" + id);
            return JsonConvert.DeserializeObject<DonorModel>(response);
        }

        public async Task<OfficerModel> GetOfficer(string id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.Token);
            var response = await httpClient.GetStringAsync(apiUrl + "repo/officer/" + id);
            return JsonConvert.DeserializeObject<OfficerModel>(response);
        }

        public async Task<BloodOption> GetBloodType(string id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.Token);
            var response = await httpClient.GetStringAsync(apiUrl + "repo/blood-type/" + id);
            return JsonConvert.DeserializeObject<BloodOption>(response);
        }
        public async Task<List<DonorOrgan>> GetOrganType(string id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.Token);
            var response = await httpClient.GetStringAsync(apiUrl + "repo/organ/" + id);
            return JsonConvert.DeserializeObject<List<DonorOrgan>>(response);
        }
    }
}
