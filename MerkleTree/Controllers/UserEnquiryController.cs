using Microsoft.AspNetCore.Mvc;

namespace MerkleTree.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserEnquiryController : ControllerBase
    {
        private readonly UserEnquiryService _userEnquiryService;

        public UserEnquiryController(UserEnquiryService userEnquiryService)
        {
            _userEnquiryService = userEnquiryService;
        }

        [HttpGet("getmerklerootofuserswithbalance")]
        public IActionResult InitializeUserMerkleTree()
        {
            var userMerkleTree = _userEnquiryService.getMerkleRootOfUsers();
            if (userMerkleTree == null)
            {
                return BadRequest("Failed to initialize user Merkle tree.");
            }
            return Ok(userMerkleTree);
        }

        [HttpGet("recalculatemerklerootofuserswithbalance")]
        public IActionResult ReCalculateUserMerkleTree()
        {
            var userMerkleTree = _userEnquiryService.ReCalculateMerkleRootOfUsers();
            if (userMerkleTree == null)
            {
                return BadRequest("Failed to re-calculate user Merkle tree.");
            }
            return Ok(userMerkleTree);
        }
    }
}
