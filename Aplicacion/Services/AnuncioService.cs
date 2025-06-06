using ApiFacebook.Dominio.Contracts;
using ApiFacebook.Dominio.Entities;

public class AnuncioService
{
    private readonly IAnuncioRepository _anuncioRepository;

    public AnuncioService(IAnuncioRepository anuncioRepository)
    {
        _anuncioRepository = anuncioRepository;
    }

    public async Task CrearAnuncioEnFacebookAsync(Anuncio anuncio)
    {
        // Aquí puedes agregar validaciones de negocio si es necesario
        await _anuncioRepository.CrearAnuncioFacebookAsync(anuncio);
    }

    public async Task PublicarEnFanpageAsync(string mensaje)
    {
        await _anuncioRepository.PublicarEnFanpageAsync(mensaje);
    }

    public async Task PublicarEnFanpageAsync(string mensaje, string link = null, string photoId = null)
    {
        await _anuncioRepository.PublicarEnFanpageAsync(mensaje, link, photoId);
    }

    public async Task<string> SubirImagenAFanpageAsync(string accessTokenPage, string imageUrl, string mensaje = null)
    {
        return await _anuncioRepository.SubirImagenAFanpageAsync(accessTokenPage, imageUrl, mensaje);
    }
}