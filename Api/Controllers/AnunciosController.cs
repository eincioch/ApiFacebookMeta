using ApiFacebook.Dominio.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AnunciosController : ControllerBase
{
    private readonly AnuncioService _anuncioService;

    public AnunciosController(AnuncioService anuncioService)
    {
        _anuncioService = anuncioService;
    }

    [HttpPost("facebook")]
    public async Task<IActionResult> CrearAnuncioFacebook([FromBody] Anuncio anuncio)
    {
        await _anuncioService.CrearAnuncioEnFacebookAsync(anuncio);
        return Ok(new { message = "Anuncio enviado a Facebook correctamente." });
    }

    [HttpPost("fanpage/post")]
    public async Task<IActionResult> PublicarEnFanpage([FromBody] string mensaje)
    {
        await _anuncioService.PublicarEnFanpageAsync(mensaje);
        return Ok(new { message = "Post publicado en la fanpage correctamente." });
    }

    [HttpPost("fanpagepersonalizado/post")]
    public async Task<IActionResult> PublicarEnFanpagePersonalizado([FromBody] PublicarFanpageDto dto)
    {
        await _anuncioService.PublicarEnFanpageAsync(dto.Mensaje, dto.Link, dto.PhotoId);
        return Ok(new { message = "Post publicado en la fanpage correctamente." });
    }

    [HttpPost("fanpage/upload-image")]
    public async Task<IActionResult> SubirImagenAFanpage([FromBody] SubirImagenDto dto)
    {
        var photoId = await _anuncioService.SubirImagenAFanpageAsync(dto.AccessTokenPage, dto.ImageUrl, dto.Mensaje);
        return Ok(new { photoId });
    }

}

public class PublicarFanpageDto
{
    public string Mensaje { get; set; }
    public string Link { get; set; }
    public string PhotoId { get; set; }
}

public class SubirImagenDto
{
    public string AccessTokenPage { get; set; }
    public string ImageUrl { get; set; }
    public string Mensaje { get; set; }
}