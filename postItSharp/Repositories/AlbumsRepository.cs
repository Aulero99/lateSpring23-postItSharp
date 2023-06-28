namespace postItSharp.Repositories;

public class AlbumsRepository
{
  private readonly IDbConnection _db;

  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Album CreateAlbum(Album albumData)
  {
    string sql = @"
    INSERT INTO albums
    (title, category, coverImg, archived, creatorId)
    VALUES
    (@title, @category, @coverImg, @archived, @creatorId);

    SELECT
      alb.*,
      creator.*
    FROM albums alb
    JOIN accounts creator ON alb.creatorId = creator.id
    WHERE alb.id = LAST_INSERT_ID();
    ";
    // NOTE ................ ⬇️ first select...............⬇️
    //...............................⬇️second select..............⬇️ 
    // .....................................⬇️return type
    Album album = _db.Query<Album, Account, Album>(sql, (album, creator) =>
    {
      //first.........second
      album.Creator = creator;
      return album; // ⬅️ return type

    }, albumData).FirstOrDefault();
    return album;
  }

  internal List<Album> GetAllAlbums()
  {
    string sql = @"
    SELECT
    alb.*,
    creator.*
    FROM albums alb
    JOIN accounts creator ON alb.creatorId = creator.id;
    ";
    List<Album> albums = _db.Query<Album, Account, Album>(sql, (album, creator) =>
    {
      album.Creator = creator;
      return album;
    }).ToList();
    return albums;
  }

  internal Album GetById(int albumId)
  {
    string sql = @"
    SELECT
    alb.*,
    creator.*
    FROM albums alb
    JOIN accounts creator ON alb.creatorId = creator.id
    WHERE alb.id = @albumId;
    ";
    Album album = _db.Query<Album, Account, Album>(sql, (album, creator) =>
    {
      album.Creator = creator;
      return album;
    }, new { albumId }).FirstOrDefault();
    return album;
  }

  internal void UpdateAlbum(Album album)
  {
    string sql = @"
    UPDATE albums SET
    title = @title,
    category = @category,
    coverImg = @coverImg,
    archived = @archived
    WHERE id = @id;
    ";

    _db.Execute(sql, album);
  }
}
