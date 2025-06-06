using Dominio.Contracts;
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
}
