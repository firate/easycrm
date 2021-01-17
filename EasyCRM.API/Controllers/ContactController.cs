using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EasyCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {



        public ContactController()
        {
           
        }

        //[HttpGet]
        //public async Task<IActionResult> SearchContacts([FromQuery] ContactParams contactParams)
        //{
        //    try
        //    {
        //        throw new NotImplementedException();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //[HttpGet("{id}", Name = "GetContact")]
        //public async Task<IActionResult> GetContactById(int contactId)
        //{

        //    try
        //    {
        //        throw new NotImplementedException();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateNewContact([FromBody] ContactCreationDTO contactCreationDTO)
        //{
        //    try
        //    {
        //        throw new NotImplementedException();

        //    }
        //    catch (SqlException e)
        //    {
        //        logger.LogError(e.InnerException.InnerException.Message);
        //        throw e;
        //    }


        //    catch (Exception e)
        //    {
        //        logger.LogError($"hata mesajım: { e.InnerException.Message}");
        //        throw e.InnerException;
        //    }
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> EditContact([FromBody]ContactEditDTO contactEditDTO, int id) 
        //{
        //    try
        //    {
        //        throw new NotImplementedException();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    throw new NotImplementedException();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteContact(int id)
        //{

        //    try
        //    {
        //        throw new NotImplementedException();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

    }
}
