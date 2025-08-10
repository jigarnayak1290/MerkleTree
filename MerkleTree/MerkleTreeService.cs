using MerkleTree.Models;
using System.Security.Cryptography;
using System.Text;

namespace MerkleTree
{
    public class MerkleTreeService
    {
        //Hash tag (text) used for leaf hash & branch hash
        private const string HashTagForLeafNBranch = "Bitcoin_Transaction";

        /// <summary>
        /// Create merkle tree from received leaf nodes
        /// </summary>
        /// <param name="_leafNodes"></param>
        /// <returns>Merkle root hash</returns>
        public string? CalculateMerkleRoot(IEnumerable<string> _leafNodes)
        {
            if (_leafNodes == null || !_leafNodes.Any())
            {
                return null;
            }

            //Calculate hash of individual leaf node
            List<MerkleTreeNode> MerkleLeafNodes = _leafNodes.Select(
                t => new MerkleTreeNode(
                    Hash: ToHexString(HashTransactionWithTag(HashTagForLeafNBranch, Encoding.UTF8.GetBytes(t)))
                )).ToList();

            //Make Merkle tree from hashed leaf nodes
            return MakeMerkleTree(HashTagForLeafNBranch, MerkleLeafNodes).Hash;
        }

        /// <summary>
        /// Generate merkletree from given leaf nodes
        /// </summary>
        /// <param name="transactions"></param>
        /// <returns>Merkle node</returns>
        private MerkleTreeNode MakeMerkleTree(string hashTag, IEnumerable<MerkleTreeNode> transactions)
        {
            //Check for single transaction, if so return as root.
            if (transactions.Count() == 1)
            {
                return transactions.Single();
            }

            //if transactions are odd than duplicate last transcation to make it even (As per Merkle tree rule)
            if (transactions.Count() % 2 != 0)
            {
                transactions = balancedMerkleTree(transactions);
            }

            var mergedTransaction = transactions
                .Chunk(2)
                .Select(pair =>
                {
                    var left = pair.First();
                    var right = pair.Last();
                    var leftBytes = FromHexString(left.Hash);
                    var rightBytes = FromHexString(right.Hash);
                    var mergedBytes = leftBytes.Concat(rightBytes).ToArray();

                    var hashTransactionWithTag = HashTransactionWithTag(hashTag, mergedBytes);

                    return new MerkleTreeNode(ToHexString(hashTransactionWithTag), left, right);
                }).ToList();

            //Recursively calling function with merged transaction list
            return MakeMerkleTree(hashTag, mergedTransaction);
        }

        /// <summary>
        /// Convert byte array to hex string
        /// </summary>
        /// <param name="cryptoTransactions"></param>
        /// <returns>Hex strings</returns>
        private string ToHexString(byte[] cryptoTransactions) =>
            BitConverter.ToString(cryptoTransactions).Replace("-", "").ToLowerInvariant();

        /// <summary>
        /// Convert hex string to byte array
        /// </summary>
        /// <param name="hex"></param>
        /// <returns>byte array of Hex string</returns>
        private byte[] FromHexString(string hex) => Enumerable.Range(0, hex.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            .ToArray();

        /// <summary>
        /// Hash transaction data in format of BIP340 with SHA256 (Hash(tag) + Hash(tag) + transaction)
        /// </summary>
        /// <param name="cryptoTransactions"></param>
        /// <returns>Hash of hash & transaction</returns>
        private byte[] HashTransactionWithTag(string hashTag, byte[] cryptoTransaction)
        {
            var tagBytes = Encoding.UTF8.GetBytes(hashTag);
            var tagHash = SHA256.HashData(tagBytes);

            var concateTagHashWithTransaction = tagHash.Concat(tagHash).Concat(cryptoTransaction).ToArray();
            return SHA256.HashData(concateTagHashWithTransaction);
        }

        /// <summary>
        ///  Balance merkle tree by duplicate last transcation to make it even (If its odd)
        /// </summary>
        /// <param name="cryptoTransactions"></param>
        /// <returns>Even merkle leaf nodes</returns>
        private IEnumerable<MerkleTreeNode> balancedMerkleTree(IEnumerable<MerkleTreeNode> cryptoTransactions)
        {
            if (cryptoTransactions.Count() % 2 != 0)
            {
                var listMerkleTreeLeafs = new List<MerkleTreeNode>();

                listMerkleTreeLeafs = cryptoTransactions.ToList();
                listMerkleTreeLeafs.Add(cryptoTransactions.Last());
                return listMerkleTreeLeafs;
            }

            return cryptoTransactions;
        }
    }
}
