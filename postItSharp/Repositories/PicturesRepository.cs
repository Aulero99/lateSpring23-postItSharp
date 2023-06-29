namespace postItSharp.Repositories;

public class PicturesRepository
{
    private readonly IDbConnection _db;

    public PicturesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Picture CreatePicture(Picture pictureData)
    {
        string sql = @"
       INSERT INTO pictures
       (imgUrl, creatorId, albumId)
       VALUES
       (@imgUrl, @creatorId, @albumId);
       
       SELECT
       pic.*,
       act.*
       FROM pictures pic
       JOIN accounts act ON act.id = pic.creatorId
       WHERE pic.id = LAST_INSERT_ID()
       ;";

        //    .............................⬇️ first arg: first select from table
        //    .....................................⬇️ second arg: 2nd select from table
        //    ..............................................⬇️ third arg: return type (this will match the return type of your method)
        Picture newPicture = _db.Query<Picture, Account, Picture>(sql, (picture, account) =>
        {
            // NOTE this section is for formatting the data we grabbed from the DB into the object we want to return
            picture.Creator = account;
            return picture;
        }, pictureData).FirstOrDefault();
        return newPicture;
    }
}