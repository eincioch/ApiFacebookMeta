using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dominio.Contracts;

public class FacebookAuthRepository : IFacebookAuthRepository
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public FacebookAuthRepository(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> RenovarTokenLargoPlazoAsync(string shortLivedToken)
    {
        var appId = _configuration["Facebook:AppId"];
        var appSecret = _configuration["Facebook:AppSecret"];
        var url = $"https://graph.facebook.com/v23.0/oauth/access_token" +
                  $"?grant_type=fb_exchange_token" +
                  $"&client_id={appId}" +
                  $"&client_secret={appSecret}" +
                  $"&fb_exchange_token={shortLivedToken}";

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error Facebook API: {response.StatusCode} - {errorContent}");
        }
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var longLivedToken = doc.RootElement.GetProperty("access_token").GetString();
        return longLivedToken;
    }

    public async Task<bool> VerificarPermisosAsync(string accessToken)
    {
        var url = $"https://graph.facebook.com/v23.0/me/permissions?access_token={accessToken}";
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var doc = JsonDocument.Parse(json);

        var permisos = doc.RootElement.GetProperty("data")
            .EnumerateArray()
            .Where(p =>
                (p.GetProperty("permission").GetString() == "pages_manage_posts" ||
                 p.GetProperty("permission").GetString() == "pages_read_engagement") &&
                p.GetProperty("status").GetString() == "granted")
            .Select(p => p.GetProperty("permission").GetString())
            .ToList();

        return permisos.Contains("pages_manage_posts") && permisos.Contains("pages_read_engagement");
    }

    public async Task<string> ObtenerTokenPaginaAsync(string userAccessToken, string pageId)
    {
        var url = $"https://graph.facebook.com/v23.0/me/accounts?access_token={userAccessToken}";
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var pages = doc.RootElement.GetProperty("data").EnumerateArray();

        foreach (var page in pages)
        {
            if (page.GetProperty("id").GetString() == pageId)
            {
                return page.GetProperty("access_token").GetString();
            }
        }
        throw new Exception("No se encontró el token de la página.");
    }
}