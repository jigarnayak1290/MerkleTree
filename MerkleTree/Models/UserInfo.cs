namespace MerkleTree.Models
{
    /// <summary>
    /// User information
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="Balance"></param>
    public record UserInfo(
        int? UserID = null,
        int? Balance = null);
}
