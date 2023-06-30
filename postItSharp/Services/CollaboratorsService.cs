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

    internal void DeleteCollaborator(int collabId, string userId)
    {
        Collaborator collab = GetById(collabId);
        if (collab.AccountId != userId) new Exception("Unauthorized to remove this collaborator!");
        int rows = _repo.DeleteCollaborator(collabId);
        if (rows > 1) new Exception("Something went wrong");
    }

    internal Collaborator GetById(int collabId)
    {
        Collaborator collab = _repo.GetById(collabId);
        if (collab == null) new Exception("Invalid id");
        return collab;
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