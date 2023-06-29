namespace postItSharp.Models;

public class Album
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public bool Archived { get; set; }
    public string coverImg { get; set; }
    public string CreatorId { get; set; }
    public Account Creator { get; set; }
}

public class CollaboratorAlbum : Album
{
    public int CollaborationId { get; set; }
}