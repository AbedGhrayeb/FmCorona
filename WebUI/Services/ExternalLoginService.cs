using Application.Interfaces;
using Application.Interfaces.ExternalAuth;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebUI.Services
{
    public class ExternalLoginService : IExternalLoginService
    {
        private const string FacebookUserInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,picture,email&access_token={0}";

        private const string GoogleUserInfoUrl = "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={0}";
        private readonly IHttpClientFactory _httpClientFactory;

        public ExternalLoginService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FacebookUserInfoResult> GetFacebookInfoasync(string accessToken)
        {
            string formattedUrl = string.Format(FacebookUserInfoUrl, accessToken);
            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }
            result.EnsureSuccessStatusCode();

            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FacebookUserInfoResult>(responseAsString);
        }

        public async Task<GoogleUserInfo> GetGoogleInfoasync(string accessToken)
        {
            string formattedUrl = string.Format(GoogleUserInfoUrl, accessToken);
            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GoogleUserInfo>(responseAsString);
        }
    }
}
