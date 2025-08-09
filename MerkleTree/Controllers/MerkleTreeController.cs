using Microsoft.AspNetCore.Mvc;

namespace MerkleTree.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MerkleTreeController : ControllerBase
    {
        private readonly MerkleTree _merkleTree;

        public MerkleTreeController(MerkleTree merkleTree)
        {
            _merkleTree = merkleTree;
        }

        [HttpPost("make-tree")]
        public IActionResult MakeMerkleTree([FromBody] List<string> transactions)
        {
            if (transactions == null || !transactions.Any())
            {
                return BadRequest("Transactions can not be empty.");
            }

            var rootHash = _merkleTree.CalculateMerkleRoot(transactions);

            //If null return from calculation then handle with gracefull return
            if (rootHash == null)
            {
                return BadRequest("Could not calculate Merkle root from given transactions.");
            }

            return Ok(new { MerkleRoot = rootHash });
        }
    }
}
