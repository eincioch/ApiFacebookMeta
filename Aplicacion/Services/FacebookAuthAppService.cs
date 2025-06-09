using Dominio.Contracts;
using Dominio.Entities;
using System.Threading.Tasks;

namespace Aplicacion.Services;

public class FacebookAuthAppService
{
    private readonly IFacebookAuthRepository _facebookAuthService;

    public FacebookAuthAppService(IFacebookAuthRepository facebookAuthService)
    {
        _facebookAuthService = facebookAuthService;
    }

    public async Task<string> RenovarTokenLargoPlazoAsync(string shortLivedToken)
    {
        return await _facebookAuthService.RenovarTokenLargoPlazoAsync(shortLivedToken);
    }

    public async Task<string> ObtenerAccessTokenPageId(string userAccessToken, string pageId) {

        return await _facebookAuthService.ObtenerTokenPaginaAsync(userAccessToken, pageId);

    }

    public async Task<List<FanPage>> ObtenerListadoFanPage(string userAccessToken)
    {

        return await _facebookAuthService.ListaPaginasAsync(userAccessToken);

    }
}
