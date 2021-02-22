
using Microsoft.AspNetCore.Mvc;
using EasyCRM.Business.Managers.Abstract;
using AutoMapper;

using System.Threading.Tasks;
using System;
using EasyCRM.Entity.Models;
using EasyCRM.Utility;
using System.Collections.Generic;
using EasyCRM.Business.ModelHelpers;
using EasyCRM.API.DTOs;

namespace EasyCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManager accountManager;
        private readonly IAddressManager addressManager;
        private readonly IMapper mapper;


        public AccountsController(IAccountManager _accountManager,IAddressManager _addressManager, IMapper _mapper)
        {
            accountManager = _accountManager;
            addressManager = _addressManager;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> SearchAccounts([FromQuery] AccountParams accountParams)
        {
            try
            {
                var accounts = await accountManager.SearchAccounts(accountParams);

                if(accounts != null )
                {
                    PagedList<AccountToReturnDTO> accountToReturns = new PagedList<AccountToReturnDTO>();

                    foreach(var acc in accounts)
                    {
                        var contacts = acc.Contacts;
                        List<ContactReturnDTO> contactsToReturn = new List<ContactReturnDTO>();
                        
                        foreach(var c in contacts)
                        {
                            ContactReturnDTO contact = new ContactReturnDTO()
                            {
                                ContactId=c.ContactId,
                                FirstName = c.FirstName,
                                MiddleName = c.MiddleName,
                                LastName= c.LastName,
                                NameTitle= c.NameTitle,
                                SelectedAddressId=(int)c.SelectedAddressId,
                                AccountId=c.AccountId.Value,
                                Notes = c.Notes
                            };
                            contactsToReturn.Add(contact);
                        }

                        AccountToReturnDTO accountsToReturn = new AccountToReturnDTO
                        {
                            AccountId=acc.AccountId,
                            OrganizationName= acc.OrganizationName,
                            AccountType=acc.AccountType.Name,
                            Description= acc.Description,
                            CreatedAt= acc.CreatedAt,
                            UpdatedAt =acc.UpdatedAt,
                            Contacts = contactsToReturn
                        };
                        accountToReturns.Add(accountsToReturn);
                    }

                    Response.AddPagination(accountToReturns.CurrentPage, accountToReturns.PageSize, accountToReturns.TotalCount, accountToReturns.TotalPages);
                    return Ok(accountToReturns);
                }
                return NotFound("Not Found!");
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet("{id}", Name = "GetAccount")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            try
            {
                Account acc= await accountManager.GetAccount(id);
                if(acc != null)
                {

                    var accountToReturn = mapper.Map<AccountToReturnDTO>(acc);
                    return Ok(accountToReturn);
                }

                return NotFound("Sorry, Account Not Found!");
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost("/new")]
        public async Task<IActionResult> CreateNewAccount([FromBody] AccountCreationDTO accountForCreation)
        {
            try
            {
                var acc = mapper.Map<Account>(accountForCreation);
                var result = await accountManager.CreateAccount(acc);

                if (result == true)
                {
                    var accountToReturn = mapper.Map<AccountToReturnDTO>(acc);
                    return CreatedAtRoute("GetAccount", new { Id = accountToReturn.AccountId }, accountToReturn);
                }

                return BadRequest("Given Account could not be added.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAccountInfo(int id, [FromBody]AccountEditDTO accountEditDTO)
        {
            try
            {
                var account = mapper.Map<Account>(accountEditDTO);
                var isEdited = await accountManager.EditAccount(id, account);
                if(isEdited == true)
                {
                    return NoContent();
                }
                return BadRequest();
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            try
            {
                var account = await accountManager.GetAccount(id);
                if(account != null)
                {
                    var isDeleted = await accountManager.DeleteAccount(id);
                    if (isDeleted == true)
                    {
                        return NoContent();
                    }
                    return BadRequest("Account could not be deleted!");
                }
                else
                {
                    return NotFound("No account found with given id!");
                }               
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("{accountId}/addAddress")]
        public async Task<IActionResult> CreateAccountAddress([FromBody] AddressCreationDTO addressCreationDTO)
        {
            try
            {
                var acc = await accountManager.GetAccount(addressCreationDTO.AccountId);

                if (acc != null)
                {
                    var address = mapper.Map<Address>(addressCreationDTO);
                    var result = await addressManager.CreateAddress(address);

                    if (result == true)
                    {
                        return NoContent();
                    }
                    else
                    {
                        throw new Exception("Address could not added!");
                    }

                }
                else
                {
                    throw new Exception("Invalid Account Id!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //[HttpPost("{accountId}/addCommInfo")]
        //public async Task<IActionResult> AddCommInfo([FromBody] CommInfoParams commInfoParams)
        //{
        //    throw new NotImplementedException();

        //}

        //[HttpDelete("{accountId}/deleteAddressInfo/{addressId}")]
        //public async Task<IActionResult> DeleteAddressInfo(int accountId, int addressId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}