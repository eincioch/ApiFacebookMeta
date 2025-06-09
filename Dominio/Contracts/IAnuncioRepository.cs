using System.Threading.Tasks;
using ApiFacebook.Dominio.Entities;

namespace ApiFacebook.Dominio.Contracts
{
    public interface IAnuncioRepository
    {
        Task CrearAnuncioFacebookAsync(Anuncio anuncio);

        Task PublicarEnFanpageAsync(string accessTokenPage, string pageId, string mensaje);

        Task<string> PublicarEnFanpageAsync(string accessTokenPage, string pageId, string mensaje, string link = null, string photoId = null);

        Task<string> SubirImagenAFanpageAsync(string accessTokenPage, string imageUrl, string mensaje = null);
        Task<string> SubirImagenLocalAFanpageAsync(string accessTokenPage, Stream imageStream, string fileName, string mensaje = null);
        Task<string> ObtenerUrlImagenAsync(string photoId, string pageAccessToken);
    }
}