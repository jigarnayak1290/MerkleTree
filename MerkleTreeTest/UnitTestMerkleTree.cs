using MerkleTree;

namespace MerkleTreeTest
{
    public class UnitTestMerkleTree
    {
        [Fact]
        public void Test_Transactions()
        {
            // Arrange test data (Example transactions to be hashed)
            // In a real-world scenario, these would be actual transaction data.
            List<string> transactions = new List<string>
            { "aaa", "bbb", "ccc", "ddd", "eee" };

            // Act (calculate the Merkle root)
            var service = new MerkleTreeService();
            var result = service.CalculateMerkleRoot(transactions);

            // Assert (Checking the root hash with result)
            var rootHash = result.Hash;
            Assert.Equal("dcb02195eff6aec28ce85b905f0be360263377e4bf7152a94c94326a944551e1", rootHash);
        }

        [Fact]
        public void Test_UerIdWithBalance()
        {
            // Arrange test data (Example transactions to be hashed)
            // In a real-world scenario, these would be actual transaction data.
            List<string> transactions = new List<string>
            { "(1,1111)", "(2,2222)", "(3,3333)", "(4,4444)", "(5,5555)", "(6,6666)", "(7,7777)", "(8,8888)" };

            // Act (calculate the Merkle root)
            var service = new MerkleTreeService();
            var result = service.CalculateMerkleRoot(transactions);

            // Assert (Checking the root hash with result)
            var rootHash = result.Hash;
            Assert.Equal("b1231de33da17c23cebd80c104b88198e0914b0463d0e14db163605b904a7ba3", rootHash);
        }
    }
}