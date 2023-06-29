namespace postItSharp.Services;

public class PicturesService
{
    private readonly PicturesRepository _repo;

    public PicturesService(PicturesRepository repo)
    {
        _repo = repo;
    }

    internal Picture CreatePicture(Picture pictureData)
    {
        Picture newPicture = _repo.CreatePicture(pictureData);
        return newPicture;
        // return _repo.CreatePicture(pictureData);
    }
}