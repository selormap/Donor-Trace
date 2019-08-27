using DonorTraceMobile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DonorTraceMobile.Services
{
    public class ApiService
    {
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
            

            ///My login
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
    }
}
