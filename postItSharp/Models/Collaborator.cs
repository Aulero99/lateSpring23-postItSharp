namespace postItSharp.Models;

public class Collaborator
{
    public int Id { get; set; }
    public string AccountId { get; set; }
    public int AlbumId { get; set; }
}


// NOTE: our view models are in the account and the album model files respectively
