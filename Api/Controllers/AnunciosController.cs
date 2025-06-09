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

    [HttpPost("fanpage/post-texto")]
    public async Task<IActionResult> PublicarEnFanpage([FromBody] PublicarFanpageTextoDto dto)
    {
        await _anuncioService.PublicarEnFanpageAsync(dto.AccessTokenPage, dto.PageId, dto.Mensaje);
        return Ok(new { message = "Post publicado en la fanpage correctamente." });
    }

    [HttpPost("fanpage/post-personalizado")]
    public async Task<IActionResult> PublicarEnFanpagePersonalizado([FromBody] PublicarFanpageDto dto)
    {
        await _anuncioService.PublicarEnFanpageAsync(dto.AccessTokenPage, dto.PageId, dto.Mensaje, dto.Link, dto.PhotoId);
        return Ok(new { message = "Post publicado en la fanpage correctamente." });
    }

    [HttpPost("fanpage/upload-image")]
    public async Task<IActionResult> SubirImagenAFanpage([FromBody] SubirImagenDto dto)
    {
        var photoId = await _anuncioService.SubirImagenAFanpageAsync(dto.AccessTokenPage, dto.ImageUrl, dto.Mensaje);
        return Ok(new { photoId });
    }

    [HttpPost("fanpage/upload-local-image")]
    public async Task<IActionResult> SubirImagenLocalAFanpage(
        [FromQuery] string accessTokenPage,
        [FromForm] SubirImagenLocalFanpageForm form)
    {
        if (form.Archivo == null || form.Archivo.Length == 0)
            return BadRequest("No se envió ningún archivo.");

        if (string.IsNullOrEmpty(accessTokenPage))
            return BadRequest("El parámetro accessTokenPage es obligatorio.");

        using var stream = form.Archivo.OpenReadStream();
        var photoId = await _anuncioService.SubirImagenLocalAFanpageAsync(
            accessTokenPage,
            stream,
            form.Archivo.FileName,
            form.Mensaje ?? string.Empty);

        return Ok(new { photoId });
    }

}

public class PublicarFanpageTextoDto
{
    public required string AccessTokenPage { get; set; }
    public required string PageId { get; set; }
    public string Mensaje { get; set; }
}

public class PublicarFanpageDto
{
    public required string AccessTokenPage { get; set; }
    public required string PageId { get; set; }
    public string Mensaje { get; set; }
    public string Link { get; set; }
    public string PhotoId { get; set; }
}

public class SubirImagenDto
{
    public required string AccessTokenPage { get; set; }
    public string ImageUrl { get; set; }
    public string Mensaje { get; set; }
}

public class SubirImagenLocalFanpageForm
{
    public string? Mensaje { get; set; }
    public IFormFile? Archivo { get; set; }
}