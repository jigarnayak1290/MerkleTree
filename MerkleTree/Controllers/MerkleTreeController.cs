using Microsoft.AspNetCore.Mvc;

namespace MerkleTree.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MerkleTreeController : ControllerBase
    {
        private readonly MerkleTreeService _merkleTreeService;

        public MerkleTreeController(MerkleTreeService merkleTreeService)
        {
            _merkleTreeService = merkleTreeService;
        }

        [HttpPost("getmerkleroot")]
        public IActionResult CalculateMerkleRoot([FromBody] List<string> transactions)
        {
            if (transactions == null || transactions.Count() ==0 )
            {
                return BadRequest("Received Transactions are empty, unable to calculate merkle root for empty transaction list");
            }

            var rootHash = _merkleTreeService.CalculateMerkleRoot(transactions);

            //If null return from calculation then handle with gracefull return
            if (rootHash == null)
            {
                return BadRequest("Could not calculate Merkle root from given transactions.");
            }

            return Ok(new { MerkleRoot = rootHash });
        }
    }
}
