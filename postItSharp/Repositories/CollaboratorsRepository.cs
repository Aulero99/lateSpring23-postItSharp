namespace postItSharp.Repositories;

public class CollaboratorsRepository
{
    private readonly IDbConnection _db;

    public CollaboratorsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Collaborator CreateCollab(Collaborator collabData)
    {
        string sql = @"
        INSERT INTO collaborators
        (accountId, albumId)
        VALUES
        (@accountId, @albumId);
        SELECT LAST_INSERT_ID();
        ";
        // NOTE we use scalar to return inforatmation from the database
        // NOTE db.Query returns rows... scalar returns the individual cell
        // NOTE SELECT LAST_INSERT_ID() and execute scalar will return back the actual data (id) from the table
        int lastInsertId = _db.ExecuteScalar<int>(sql, collabData);
        collabData.Id = lastInsertId; // cast the new id from the database to the data object so we can return it back with the M:M id
        return collabData;
    }

    internal int DeleteCollaborator(int collabId)
    {
        string sql = @"
        DELETE FROM collaborators
        WHERE id = @collabId
        LIMIT 1
        ;";
        int rows = _db.Execute(sql, new { collabId });
        return rows;
    }

    internal Collaborator GetById(int collabId)
    {
        string sql = @"
        SELECT
        collabs.*
        FROM collaborators collabs
        WHERE collabs.id = @collabId
        ;";
        Collaborator collab = _db.Query<Collaborator>(sql, new { collabId }).FirstOrDefault();
        return collab;
    }

    internal List<CollaboratorAccount> GetCollaboratorsByAlbumdId(int albumId)
    {
        string sql = @"
        SELECT
        collabs.*,
        act.*
        FROM collaborators collabs
        JOIN accounts act ON act.id = collabs.accountId
        WHERE collabs.albumId = @albumId
        ;";
        List<CollaboratorAccount> collabs = _db.Query<Collaborator, CollaboratorAccount, CollaboratorAccount>(sql, (collab, account) =>
        {
            account.CollaborationId = collab.Id;
            return account;
        }, new { albumId }).ToList();
        return collabs;
    }

    internal List<CollaboratorAlbum> GetMyCollaboratorAlbums(string accountId)
    {
        string sql = @"
        SELECT
        collabs.*,
        alb.*,
        act.*
        FROM collaborators collabs
        JOIN albums alb ON alb.id= collabs.albumId
        JOIN accounts act ON act.id = alb.creatorId
        WHERE collabs.accountId = @accountId
        ;";
        List<CollaboratorAlbum> myCollabs = _db.Query<Collaborator, CollaboratorAlbum, Account, CollaboratorAlbum>(sql, (collab, album, account) =>
        {
            album.CollaborationId = collab.Id;
            album.Creator = account;
            return album;
        }, new { accountId }).ToList();
        return myCollabs;
    }
}