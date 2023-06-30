namespace postItSharp.Services;

public class CollaboratorsService
{
    private readonly CollaboratorsRepository _repo;

    public CollaboratorsService(CollaboratorsRepository repo)
    {
        _repo = repo;
    }

    internal Collaborator CreateCollab(Collaborator collabData)
    {
        Collaborator newCollab = _repo.CreateCollab(collabData);
        return newCollab;
    }

    internal List<CollaboratorAccount> GetCollaboratorsByAlbumId(int albumId)
    {
        List<CollaboratorAccount> collabs = _repo.GetCollaboratorsByAlbumdId(albumId);
        return collabs;
    }

    internal List<CollaboratorAlbum> GetMyCollaboratorAlbums(string accountId)
    {
        List<CollaboratorAlbum> myCollabs = _repo.GetMyCollaboratorAlbums(accountId);
        return myCollabs;
    }
}