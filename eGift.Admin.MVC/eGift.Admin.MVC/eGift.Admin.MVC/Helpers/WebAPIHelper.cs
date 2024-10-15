using System.Text;

namespace eGift.Admin.MVC.Helpers
{
    public static class WebAPIHelper
    {
        #region Variables

        private static IConfiguration _configuration;

        // Create a single static HttpClient instance (thread-safe)
        private static readonly HttpClient _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(300) // Optional timeout setting
        };

        //Web API Base Url
        static string baseUrl = "";

        #endregion

        #region Constructors

        static WebAPIHelper()
        {
            _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            baseUrl = _configuration[$"APIUrl"];
        }

        #endregion

        #region Web Client Methods

        // Static GET method
        public static async Task<string> GetWebAPIClient(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(baseUrl + url);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }

        // Static POST method
        public static async Task<string> PostWebAPIClient(string url, string jsonData)
        {
            try
            {
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(baseUrl + url, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }

        // Static PUT method
        public static async Task<string> PutWebAPIClient(string url, string jsonData)
        {
            try
            {
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync(baseUrl + url, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }

        // Static DELETE method
        public static async Task<string> DeleteWebAPIClient(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(baseUrl + url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }

        #endregion
    }
}
