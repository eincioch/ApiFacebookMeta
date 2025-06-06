using ApiFacebook.Dominio.Contracts;
using ApiFacebook.Dominio.Entities;

using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

public class AnuncioRepository : IAnuncioRepository
{
    private readonly HttpClient _httpClient;
    private readonly string _accessToken;
    private readonly string _adAccountId;
    private readonly string _pageId;
    private readonly string _accessTokenPage;

    public AnuncioRepository(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _accessToken = configuration["Facebook:AccessToken"];
        _adAccountId = configuration["Facebook:AdAccountId"];
        _pageId = configuration["Facebook:PageId"];
        _accessTokenPage = configuration["Facebook:AccessTokenPage"];
    }

    public async Task CrearAnuncioFacebookAsync(Anuncio anuncio)
    {
        var url = $"https://graph.facebook.com/v23.0/act_{_adAccountId}/ads?access_token={_accessToken}";
        var body = new
        {
            name = anuncio.Nombre,
            adset_id = anuncio.AdsetId,
            creative = new { creative_id = anuncio.CreativeId },
            status = anuncio.Estado ?? "PAUSED"
        };
        var response = await _httpClient.PostAsJsonAsync(url, body);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error Facebook API: {response.StatusCode} - {errorContent}");
        }
        response.EnsureSuccessStatusCode();
    }

    public async Task PublicarEnFanpageAsync(string mensaje)
    {
        //ver Fanpage https://graph.facebook.com/v23.0/me/accounts?access_token=EAADiSzrEAlUBO90pfAZAidA3xQbOK2dtCSZBb8Bobr8ZAloN9HpTrMX6XdPMBJh608PfQRZBmwd3Lxssm2gkQ7as2RAqH2c8aXky2edWGZCkK3eulUv7hZCzuAvYSZAjFgVfu4K8kFl8bRFCBC38xeuO6NjvQy6ZCFUQvojyZBirwZCPGTKkgG7EBbQPjT8cKh3REZCZAQjSCzqWrvN5iGzZCXIgCHgVgj6XcSjqHvWLh

        var url = $"https://graph.facebook.com/v23.0/{_pageId}/feed?access_token={_accessTokenPage}";
        var body = new
        {
            message = mensaje
        };
        var response = await _httpClient.PostAsJsonAsync(url, body);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error Facebook API: {response.StatusCode} - {errorContent}");
        }
        response.EnsureSuccessStatusCode();
    }

    public async Task PublicarEnFanpageAsync(string mensaje, string link = null, string photoId = null)
    {
        var url = $"https://graph.facebook.com/v23.0/{_pageId}/feed?access_token={_accessTokenPage}";
        var body = new Dictionary<string, object>
    {
        { "message", mensaje }
    };

        if (!string.IsNullOrEmpty(link))
            body.Add("link", link);

        if (!string.IsNullOrEmpty(photoId))
            body.Add("attached_media", new[] { new { media_fbid = photoId } });

        var response = await _httpClient.PostAsJsonAsync(url, body);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error Facebook API: {response.StatusCode} - {errorContent}");
        }
        response.EnsureSuccessStatusCode();
    }

    public async Task<string> SubirImagenAFanpageAsync(string imageUrl, string mensaje = null)
    {
        var url = $"https://graph.facebook.com/v23.0/{_pageId}/photos?access_token={_accessToken}";

        var parametros = new Dictionary<string, string>
        {
            { "url", imageUrl },
            { "published", "false" } // No publicar directamente, solo subir
        };
        if (!string.IsNullOrEmpty(mensaje))
            parametros.Add("message", mensaje);

        var content = new FormUrlEncodedContent(parametros);
        var response = await _httpClient.PostAsync(url, content);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error Facebook API: {response.StatusCode} - {errorContent}");
        }
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        // El ID de la foto subida está en el campo "id"
        return json.GetProperty("id").GetString();
    }
}