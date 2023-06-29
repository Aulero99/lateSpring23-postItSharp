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
}