using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Settings;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EncryptDecryptController : BaseController
    {
        public EncryptDecryptController()
        {
        }

        [HttpPost("get-encrypt")]
        public ActionResult GetEncryptText([FromBody] string plainText)
        {
            var encryptdata = EncryptDecrypt.EncryptText(plainText);
            return Ok(encryptdata);
        }

        [HttpPost("get-decrypt")]
        public ActionResult GetDecryptText([FromBody] string encryptText)
        {
            var decryptdata = EncryptDecrypt.DecryptText(encryptText);
            return Ok(decryptdata);
        }
    }
}
