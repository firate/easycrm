using AutoMapper;
using EasyCRM.API.DTOs;
using EasyCRM.Business.Managers.Abstract;
using EasyCRM.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EasyCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactManager contactManager;
        private readonly IMapper mapper;



        public ContactController(IContactManager _contactManager, IMapper _mapper)
        {
            contactManager = _contactManager;
            mapper = _mapper;

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

        [HttpGet("{id}", Name = "GetContact")]
        public async Task<IActionResult> GetContactById(int contactId)
        {

            try
            {
                var c = await contactManager.GetContact(contactId);

                if (c != null)
                {
                    var contactToReturn = mapper.Map<ContactReturnDTO>(c);
                    return Ok(contactToReturn);
                }

                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateNewContact([FromBody] ContactCreationDTO contactCreationDTO)
        {
            try
            {
                var contact = mapper.Map<Contact>(contactCreationDTO);

                var result = await contactManager.CreateContact(contact);

                if (result == true)
                {
                    var contactToReturn = mapper.Map<ContactReturnDTO>(contact);
                    return CreatedAtRoute("GetContact", new { ContactId = contactToReturn.ContactId }, contactToReturn);
                }

                return BadRequest();

            }
            catch (Exception)
            {
                throw;
            }
        }



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

        [HttpPost("/connectAddress")]
        public async Task<IActionResult> ConnectAddressToContact(int contactId, int addressId)
        {
            try
            {
                var result = await contactManager.ConnectAddressToContact(contactId, addressId);

                if (result == true)
                {
                    return NoContent();
                }

                return BadRequest();


            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}/addresses")]
        public async Task<IActionResult> GetContactAddresses(int id)
        {

            try
            {
                var c = await contactManager.GetContactAddresses(id);

                if (c != null)
                {
                    return Ok(c);
                }

                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}

