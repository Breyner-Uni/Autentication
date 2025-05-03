using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SampleMvcApp.Service
{
    public class TokenService : ITokenService
    {
        public async Task<string> GetAccessToken()
        {
            var client = new HttpClient();
            var credentials = new
            {
                client_id = "fm4kjm1h8UWL7cZ8zGn6yKAj49Vd5BCe",
                client_secret = "zoVWCEITVOhhbHxY_iW3ngU3jAXJ7hy3spP8t8YV0ezluWn4R9VTydfI2Y5UbxFV",
                audience = "https://dev-kw0pbr6x4dqrvm5g.us.auth0.com/api/v2/",
                grant_type = "client_credentials"
            };



            var content = new StringContent(JsonSerializer.Serialize(credentials), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://dev-kw0pbr6x4dqrvm5g.us.auth0.com/oauth/token", content); // Sustituye por la URL de tu servicio OAuth
            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonSerializer.Deserialize<JsonElement>(json);

            // Obtener el access_token
            return obj.GetProperty("access_token").GetString();
        }
    }
}
