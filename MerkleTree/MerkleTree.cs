using System.Security.Cryptography;
using System.Text;
using MerkleTree.Models;


namespace MerkleTree
{
    public class MerkleTree
    {
        //Hash tag (text) used for leaf hash & branch hash
        private const string HashTag = "Bitcoin_Transaction";

        //public MerkleTreeNode CalculateMerkleRoot(List<string> cryptoTransactions)

        public string? CalculateMerkleRoot(IEnumerable<string> cryptoTransactions)
        {
            if(cryptoTransactions == null || !cryptoTransactions.Any())
            {
                return null;
            }

            //String cryptoTransactions to leaf nodes (byte array) via LINQ
            List<MerkleTreeNode> leafNodes = cryptoTransactions.Select(t => new MerkleTreeNode(
                Hash: ToHexString(HashTransactionWithTag(HashTag, t))
                )).ToList();

            //Make Merkle tree from crypto transactions
            return MakeMerkleTree(leafNodes).Hash;


        }

        private MerkleTreeNode MakeMerkleTree(IEnumerable<MerkleTreeNode> cryptoTransactions) 
        {
            return null;
        }

        /// <summary>
        /// Convert byte array to hex string
        /// </summary>
        /// <param name="cryptoTransactions"></param>
        /// <returns></returns>
        private string ToHexString(byte[] cryptoTransactions) =>
            BitConverter.ToString(cryptoTransactions).Replace("-", "").ToLowerInvariant();
        
        /// <summary>
        /// Convert hex string to byte array
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private byte[] FromHexString(string hex) => Enumerable.Range(0, hex.Length)
            .Where(x=> x % 2 == 0)
            .Select(x => Convert.ToByte(hex.Substring(x, 2),16))
            .ToArray();

        /// <summary>
        /// Hash transaction data in format of BIP340 with SHA256 (Hash(tag) + Hash(tag) + transaction)
        /// </summary>
        /// <param name="cryptoTransactions"></param>
        /// <returns></returns>
        private byte[] HashTransactionWithTag(string hashTag, string cryptoTransaction)
        {
            using(var sha256 = SHA256.Create())
            {
                var tagBytes = Encoding.UTF8.GetBytes(hashTag);
                var tagHash = sha256.ComputeHash(tagBytes);
                var transactionBytes = Encoding.UTF8.GetBytes(cryptoTransaction);

                var concateTagHashWithTransaction = tagHash.Concat(tagHash).Concat(transactionBytes).ToArray();
                return sha256.ComputeHash(concateTagHashWithTransaction);
            }
        }
        
    }
}
