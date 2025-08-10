namespace MerkleTree.Models
{
    /// <summary>
    /// Single node information of Merkle tree (As Type Record for immutability)
    /// </summary>
    public record MerkleTreeNode(
        string Hash, 
        MerkleTreeNode? Left = null, 
        MerkleTreeNode? Right = null);
}
