namespace postItSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollaboratorsController : ControllerBase
    {
        private readonly CollaboratorsService _collabsService;
        private readonly Auth0Provider _auth0;

        public CollaboratorsController(CollaboratorsService collabsService, Auth0Provider auth0)
        {
            _collabsService = collabsService;
            _auth0 = auth0;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Collaborator>> CreateCollab([FromBody] Collaborator collabData)
        {
            try
            {
                Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
                // NOTE: HttpContext is the C# equivalent of 'req' in Node.JS
                // NOTE we are essentially passing in the entire req or context, so then we can pass the bearer token from the request headers to Auth0
                collabData.AccountId = userInfo.Id;
                Collaborator newCollab = _collabsService.CreateCollab(collabData);
                return Ok(newCollab);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}