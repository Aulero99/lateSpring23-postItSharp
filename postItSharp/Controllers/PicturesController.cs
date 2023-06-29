namespace postItSharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PicturesController : ControllerBase
{
    private readonly PicturesService _picturesService;
    private readonly Auth0Provider _auth0;

    public PicturesController(PicturesService picturesService, Auth0Provider auth0)
    {
        _picturesService = picturesService;
        _auth0 = auth0;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Picture>> CreatePicture([FromBody] Picture pictureData)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            pictureData.CreatorId = userInfo.Id;
            Picture newPicture = _picturesService.CreatePicture(pictureData);
            return Ok(newPicture);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{pictureId}")]
    [Authorize]
    public async Task<ActionResult<string>> DeletePicture(int pictureId)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            _picturesService.DeletePicture(pictureId, userInfo.Id);
            return Ok("Picture was successfully deleted!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
