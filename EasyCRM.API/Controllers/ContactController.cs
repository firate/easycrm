using AutoMapper;
using EasyCRM.API.DTOs;
using EasyCRM.Business.Managers.Abstract;
using EasyCRM.Business.ModelHelpers;
using EasyCRM.Entity.Models;
using EasyCRM.Utility;
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

        [HttpGet]
        public async Task<IActionResult> SearchContacts([FromQuery] ContactParams contactParams)
        {
            try
            {
                var contacts = await contactManager.SearchContacts(contactParams);

                if(contacts != null)
                {
                    PagedList<ContactReturnDTO> contactsToReturn = new PagedList<ContactReturnDTO>();

                    foreach(var c in contacts)
                    {
                        ContactReturnDTO cr = new ContactReturnDTO
                        {
                            AccountId = c.AccountId,
                            ContactId = c.ContactId,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            MiddleName = c.MiddleName,
                            NameTitle = c.NameTitle,
                            Notes = c.Notes,
                            SelectedAddressId = c.SelectedAddressId
                        };
                        contactsToReturn.Add(cr);
                    }

                    Response.AddPagination(contactsToReturn.CurrentPage,contactsToReturn.PageSize,contactsToReturn.TotalCount,contactsToReturn.TotalPages);
                    return Ok(contactsToReturn);
                }
                return NotFound();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}", Name = "GetContact")]
        public async Task<IActionResult> GetContactById(int id)
        {

            try
            {
                var c = await contactManager.GetContact(id);

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

        [HttpPost("new")]
        public async Task<IActionResult> CreateNewContact([FromBody] ContactCreationDTO contactCreationDTO)
        {
            try
            {
                var contact = mapper.Map<Contact>(contactCreationDTO);

                var result = await contactManager.CreateContact(contact);

                if (result == true)
                {
                    var contactToReturn = mapper.Map<ContactReturnDTO>(contact);
                    return CreatedAtRoute("GetContact", new { Id = contactToReturn.ContactId }, contactToReturn);
                }

                return BadRequest();

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditContact([FromBody] ContactEditDTO contactEditDTO, int id)
        {
            try
            {
                var contactAsInputParam = mapper.Map<Contact>(contactEditDTO);

                var result = await contactManager.EditContact(id, contactAsInputParam);

                var contact = await contactManager.GetContact(id);

                if (result == true)
                {
                    var contactToReturn = mapper.Map<ContactReturnDTO>(contact);
                    return CreatedAtRoute("GetContact", new { id = contact.ContactId }, contactToReturn);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {

            try
            {
                var result = await contactManager.DeleteContact(id);
                if (result == true)
                {
                    return NoContent();
                }

                return BadRequest("Contact could not deleted!");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("connectAddress")]
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

