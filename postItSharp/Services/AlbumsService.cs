namespace postItSharp.Services;

public class AlbumsService
{
  private readonly AlbumsRepository _repo;

  public AlbumsService(AlbumsRepository repo)
  {
    _repo = repo;
  }

  internal Album ArchiveAlbum(int albumId, string userId)
  {
    Album album = GetById(albumId);
    if (album.CreatorId != userId) throw new Exception("That's my purse I don't know you.");

    album.Archived = !album.Archived;
    _repo.UpdateAlbum(album);
    return album;
  }

  internal Album CreateAlbum(Album albumData)
  {
    Album album = _repo.CreateAlbum(albumData);
    return album;
  }

  internal List<Album> GetAllAlbums()
  {
    List<Album> albums = _repo.GetAllAlbums();
    return albums;
  }

  internal Album GetById(int albumId)
  {
    Album album = _repo.GetById(albumId);
    if (album == null) throw new Exception($"no album at id:{albumId}. Mick if you see this in a students project they copy and pasted it.");
    return album;
  }
}
