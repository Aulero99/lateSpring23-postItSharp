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
}