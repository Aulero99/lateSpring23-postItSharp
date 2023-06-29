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

    internal Picture GetById(int pictureId)
    {
        Picture picture = _repo.GetById(pictureId);
        if (picture == null) new Exception("Invalid Id");
        return picture;
    }

    internal void DeletePicture(int pictureId, string userId)
    {
        Picture picture = GetById(pictureId);
        if (picture.CreatorId != userId) new Exception("Nacho picture");
        int rows = _repo.DeletePicture(pictureId);
        if (rows > 1) new Exception("Something went wrong....why did we delete more than one!!");
    }

    internal List<Picture> GetPicturesByAlbumId(int albumId)
    {
        List<Picture> pictures = _repo.GetPicturesByAlbumId(albumId);
        return pictures;
    }
}