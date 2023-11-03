using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Helpers;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<ActionResult> GetImage(string imageId) 
        { 
            var image = await FileHelper.GetImageByImageIdAsync(imageId);
            return Ok(image);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAudio(string fileId)
        {
            var file = await FileHelper.GetAudioByFileIdAsync(fileId);
            return Ok(file);
        }
    }
}
