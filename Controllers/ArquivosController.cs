using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace AppTurismoAPI.Controllers
{
    [Authorize]
    [Route("api/arquivos")]
    [ApiController]
    public class ArquivosController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider; //injeção do Fileextension para ler arquivos binários

        public ArquivosController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
            throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider));
        }

        [HttpGet("{arquivoId}")]
        public ActionResult GetArquivo(string arquivoId)
        {
            var caminho = "getting-started-with-rest-slides.pdf";

            if (!System.IO.File.Exists(caminho))
            {
                return NotFound();
            }

            if(!_fileExtensionContentTypeProvider.TryGetContentType(caminho, out var contentType)) // se o arquivo n tiver um header especpifico aplicar o octet-stream
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(caminho);
            return File(bytes, contentType, Path.GetFileName(caminho));
        }
    }
}
