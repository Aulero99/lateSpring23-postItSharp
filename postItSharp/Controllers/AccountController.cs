namespace postItSharp.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly CollaboratorsService _collabsService;
    private readonly Auth0Provider _auth0Provider;

    public AccountController(AccountService accountService, Auth0Provider auth0Provider, CollaboratorsService collabsService)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
        _collabsService = collabsService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_accountService.GetOrCreateProfile(userInfo));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("collaborators")]
    [Authorize]
    public async Task<ActionResult<List<CollaboratorAlbum>>> GetMyCollaboratorAlbums()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<CollaboratorAlbum> myAlbums = _collabsService.GetMyCollaboratorAlbums(userInfo.Id);
            return Ok(myAlbums);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
