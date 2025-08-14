using MerkleTree.Models;

namespace MerkleTree
{
    public class UserEnquiryService
    {
        private static MerkleTreeNode? userMerkleTree = null;

        /// <summary>
        /// Initializes the Merkle tree with user information and returns the Merkle root.
        /// </summary>
        /// <returns></returns>
        public MerkleTreeNode? getMerkleRootOfUsers()
        {
            if (userMerkleTree == null)
            {
                try
                {
                    //Initialize the Merkle tree service
                    MerkleTreeService merkleTreeService = new MerkleTreeService();

                    //Fetch user information from a data source if no user available then return null
                    List<UserInfo> userInfos = fetchUserInfos();
                    if (userInfos == null || !userInfos.Any())
                    {
                        return null; // No user information available
                    }

                    //Convert user info to string format for hashing
                    List<string> userInfoStrings = userInfoToString(userInfos);                       

                    //Build the Merkle tree from user info strings
                    userMerkleTree = merkleTreeService.CalculateMerkleRoot(userInfoStrings);
                }
                catch (Exception)
                {
                    return null;
                }                
            }
            return userMerkleTree;
        }

        /// <summary>
        /// Generally this method will be called by a scheduled job or manually when needed just after user info updates.
        /// When user information is updated, this method can be called to re-calculate the Merkle root.
        /// As per instruction, user information will be updated once in a day.
        /// (I have kept it public for demo purpose only, In Actual api it will be private)
        /// </summary>
        /// <returns></returns>
        public MerkleTreeNode? ReCalculateMerkleRootOfUsers()
        {            
            try
            {
                //Initialize the Merkle tree service
                MerkleTreeService merkleTreeService = new MerkleTreeService();

                //Fetch user information from a data source if no user available then return null
                List<UserInfo> userInfos = fetchUserInfos();
                if (userInfos == null || !userInfos.Any())
                {
                    return null; // No user information available
                }

                //Convert user info to string format for hashing
                List<string> userInfoStrings = userInfoToString(userInfos);

                //Build the Merkle tree from user info strings
                userMerkleTree = merkleTreeService.CalculateMerkleRoot(userInfoStrings);
            }
            catch (Exception)
            {
                return null;
            }
            return userMerkleTree;
        }

        private List<string> userInfoToString(List<UserInfo> userInfos)
        {
            //Convert user info to string format for hashing
            return userInfos.Select(user => $"({user.UserID},{user.Balance})").ToList();
        }

        private List<UserInfo> fetchUserInfos()
        {
            //Generate sample user data for demonstration purposes
            //(Actual implementation would involve fetching from a database or other storage)
            return
            [
                new UserInfo { UserID = 1, Balance = 1111 },
                new UserInfo { UserID = 2, Balance = 2222 },
                new UserInfo { UserID = 3, Balance = 3333 },
                new UserInfo { UserID = 4, Balance = 4444 },
                new UserInfo { UserID = 5, Balance = 5555 },
                new UserInfo { UserID = 6, Balance = 6666 },
                new UserInfo { UserID = 7, Balance = 7777 },
                new UserInfo { UserID = 8, Balance = 8888 }
            ];
        }
    }
}
