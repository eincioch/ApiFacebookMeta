using Aplicacion.Services;
using Dominio.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FacebookAuthController : ControllerBase
{
    private readonly FacebookAuthAppService _facebookAuthAppService;

    public FacebookAuthController(FacebookAuthAppService facebookAuthAppService)
    {
        _facebookAuthAppService = facebookAuthAppService;
    }

    [HttpPost("renovar-token")]
    public async Task<IActionResult> RenovarToken([FromBody] RenovarTokenDto dto)
    {
        var longLivedToken = await _facebookAuthAppService.RenovarTokenLargoPlazoAsync(dto.ShortLivedToken);
        return Ok(new { access_token = longLivedToken });
    }

    [HttpPost("obtener-lista-fanpage")]
    public async Task<IActionResult> ObtenerListaFanPage([FromBody] string userAccessToken)
    {
        var fanPages = await _facebookAuthAppService.ObtenerListadoFanPage(userAccessToken);
        // Retornar la lista de fanpages en la respuesta
        return Ok(new { fanpages = fanPages });
    }

    [HttpPost("obtener-token-page")]
    public async Task<IActionResult> ObtenerTokenPage([FromBody] DataUserDto dto)
    {
        var pageToken = await _facebookAuthAppService.ObtenerAccessTokenPageId(dto.UserAccessToken, dto.PageId);
        return Ok(new { access_token_page = pageToken });
    }
}

public class RenovarTokenDto
{
    public string ShortLivedToken { get; set; }
}

public class DataUserDto {
    public string UserAccessToken { get; set; }

    public string PageId { get; set; }
}