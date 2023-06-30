namespace postItSharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly AlbumsService _albumsService;
    private readonly PicturesService _picturesService;
    private readonly CollaboratorsService _collabsService;
    private readonly Auth0Provider _auth;

    public AlbumsController(AlbumsService albumsService, Auth0Provider auth, PicturesService picturesService, CollaboratorsService collabsService)
    {
        _albumsService = albumsService;
        _auth = auth;
        _picturesService = picturesService;
        _collabsService = collabsService;
    }

    [HttpPost]
    [Authorize]
    // NOTE Task<> is the required return type for any async method
    public async Task<ActionResult<Album>> CreateAlbum([FromBody] Album albumData)
    {
        try
        {
            // NOTE get the users information from auth0
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            // attach it to the data from the body
            albumData.CreatorId = userInfo.Id;

            Album album = _albumsService.CreateAlbum(albumData);
            return new ActionResult<Album>(Ok(album));
        }
        catch (Exception e)
        {
            return new ActionResult<Album>(BadRequest(e.Message));
        }
    }

    [HttpGet]
    public ActionResult<List<Album>> GetAllAlbums()
    {
        try
        {
            List<Album> albums = _albumsService.GetAllAlbums();
            return Ok(albums);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{albumId}")]
    public ActionResult<Album> GetById(int albumId)
    {
        try
        {
            Album album = _albumsService.GetById(albumId);
            return Ok(album);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{albumId}/pictures")]
    public ActionResult<List<Picture>> GetPicturesByAlbumId(int albumId)
    {
        try
        {
            List<Picture> pictures = _picturesService.GetPicturesByAlbumId(albumId);
            return Ok(pictures);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{albumId}/collaborators")]
    public ActionResult<List<CollaboratorAccount>> GetCollaboratorsByAlbumId(int albumId)
    {
        try
        {
            List<CollaboratorAccount> collabs = _collabsService.GetCollaboratorsByAlbumId(albumId);
            return Ok(collabs);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpDelete("{albumId}")]
    [Authorize]
    public async Task<ActionResult<Album>> ArchiveAlbum(int albumId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Album album = _albumsService.ArchiveAlbum(albumId, userInfo.Id);
            return Ok(album);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
