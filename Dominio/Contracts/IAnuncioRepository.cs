using System.Threading.Tasks;
using ApiFacebook.Dominio.Entities;

namespace ApiFacebook.Dominio.Contracts
{
    public interface IAnuncioRepository
    {
        Task CrearAnuncioFacebookAsync(Anuncio anuncio);

        Task PublicarEnFanpageAsync(string mensaje);

        Task PublicarEnFanpageAsync(string mensaje, string link = null, string photoId = null);

        Task<string> SubirImagenAFanpageAsync(string accessTokenPage, string imageUrl, string mensaje = null);
    }
}