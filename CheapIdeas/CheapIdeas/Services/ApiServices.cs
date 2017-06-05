using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CheapIdeas.Helpers;
using CheapIdeas.Models;
using Newtonsoft.Json;
using CheapIdeas.Models;
using Newtonsoft.Json.Linq;

namespace CheapIdeas.Services
{
    internal class ApiServices
    {
        public async Task<bool> RegisterUserAsync(
            string email, string password, string confirmPassword)
        {
            var client = new HttpClient();

            var model = new RegisterBindingModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                Constants.BaseApiAddress + "api/Account/Register", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post, Constants.BaseApiAddress + "Token");

            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();
            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(content);

            var accessTokenExpiration = jwtDynamic.Value<DateTime>(".expires");
            var accessToken = jwtDynamic.Value<string>("access_token");

            Settings.AccessTokenExpirationDate = accessTokenExpiration;

            Debug.WriteLine(accessTokenExpiration);

            Debug.WriteLine(content);

            return accessToken;
        }

        public async Task<List<Idea>> GetIdeasAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/ideas");

            var ideas = JsonConvert.DeserializeObject<List<Idea>>(json);

            return ideas;
        }

        public async Task PostIdeaAsync(Idea idea, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = JsonConvert.SerializeObject(idea);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(Constants.BaseApiAddress + "api/Ideas", content);
        }

        public async Task PutIdeaAsync(Idea idea, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = JsonConvert.SerializeObject(idea);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(
                Constants.BaseApiAddress + "api/Ideas/" + idea.Id, content);
        }

        public async Task DeleteIdeaAsync(int ideaId, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            
            var response = await client.DeleteAsync(
                Constants.BaseApiAddress + "api/Ideas/" + ideaId);
        }

        public async Task<List<Idea>> SearchIdeasAsync(string keyword, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(
                Constants.BaseApiAddress + "api/ideas/Search/" + keyword);

            var ideas = JsonConvert.DeserializeObject<List<Idea>>(json);

            return ideas;
        }
    }
}