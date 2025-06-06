using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Contracts
{
    public interface IFacebookAuthRepository
    {
        Task<string> RenovarTokenLargoPlazoAsync(string shortLivedToken);

        Task<bool> VerificarPermisosAsync(string accessToken);

        Task<string> ObtenerTokenPaginaAsync(string userAccessToken, string pageId);
    }
}
