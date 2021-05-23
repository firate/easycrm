
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
using System.Linq;

namespace EasyCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManager accountManager;
        private readonly IAddressManager addressManager;
        private readonly ICommunicationInfoManager commInfoManager;
        private readonly IMapper mapper;


        public AccountsController(IAccountManager _accountManager,
                                  IAddressManager _addressManager,
                                  ICommunicationInfoManager _commInfoManager,
                                  IMapper _mapper)
        {
            accountManager = _accountManager;
            addressManager = _addressManager;
            mapper = _mapper;
            commInfoManager = _commInfoManager;
        }

        #region Account Base Methods
        [HttpPost]
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

        [HttpGet("{id}", Name = "GetAccount")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            try
            {
                Account acc = await accountManager.GetAccount(id);
                if (acc == null)
                {
                    return NotFound("Sorry, Account Not Found!");
                }
                var accountGroups = acc.AccountGroups.ToList();
                
                List<Group> groups = new List<Group>();

                foreach(var ag in accountGroups)
                {
                    groups.Add(ag.Group);
                }

                
                var groupsToReturn = mapper.Map<List<GroupReturnDTO>>(groups);
               
                var accountToReturn = mapper.Map<AccountToReturnDTO>(acc);
                accountToReturn.Groups = groupsToReturn;
                
                return Ok(accountToReturn);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAccountInfo(int id, [FromBody] AccountEditDTO accountEditDTO)
        {
            try
            {
                var account = mapper.Map<Account>(accountEditDTO);

                var isEdited = await accountManager.EditAccount(id, account);
                if (isEdited == true)
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
                if (account == null)
                {
                    return NotFound("No account found with given id!");
                }

                var isDeleted = await accountManager.DeleteAccount(id);
                if (isDeleted != true)
                {
                    return BadRequest("Account could not be deleted!");
                }
                return NoContent();

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchAccounts([FromQuery] AccountParams accountParams)
        {
            try
            {
                var accounts = await accountManager.SearchAccounts(accountParams);

                if (accounts == null)
                {
                    return NotFound("Not Found!");
                }
                
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
                            SelectedAddressId=c.SelectedAddressId,
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
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region Account's Address Methods

        [HttpGet("{accountId}/addresses/{addressId}")]
        public async Task<IActionResult> GetAccountAddressByAddressId(int accountId, int addressId)
        {
            try
            {

                var address = await addressManager.GetAddress(addressId,accountId);
                if(address != null)
                {
                    var addressToReturn = mapper.Map<AddressReturnDTO>(address);
                    return Ok(addressToReturn);
                }

                return NotFound();
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
                var acc = await accountManager.GetAccount((int)addressCreationDTO.AccountId);

                if (acc == null)
                {
                    return BadRequest("Invalid Account Id!");
                }

                var address = mapper.Map<Address>(addressCreationDTO);
                var result = await addressManager.CreateAddress(address);

                if (result == true)
                {
                    return NoContent();
                    // return Created();
                }
                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{accountId}/addresses/{addressId}")]
        public async Task<IActionResult> EditAccountAddress(int accountId, int addressId, [FromBody] AddressEditDTO addressEditDTO)
        {
            try
            {
                var acc = await accountManager.GetAccount(accountId);
                if (acc != null)
                {
                    var address = mapper.Map<Address>(addressEditDTO);
                    var result = await addressManager.EditAddress(addressId,address);
                    if(result == true)
                    {
                        return NoContent();
                    }
                   
                }

                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{accountId}/addresses/{addressId}")]
        public async Task<IActionResult> DeleteAddressInfo(int accountId, int addressId)
        {
            try
            {
                var acc = await accountManager.GetAccount(accountId);

                if (acc == null)
                {
                    return NotFound("No account found with given id!");
                }


                var adr = await addressManager.GetAddress(addressId);

                if (adr == null)
                {
                    return NotFound("No address found with given id!");
                }

                if (adr.IsMain == true)
                {
                    return BadRequest("Could not delete primary address.");
                }

                var result = await addressManager.DeleteAddress(addressId);
                if (result == true)
                {
                    return NoContent();
                }

                return BadRequest("");

            }
            catch (Exception)
            {

                throw;
            }
        }

        //public async Task<IActionResult> SearchAccountAddress([FromQuery] AddressParams accountAddressParams)
        //{
        //    return null;
        //}
        
        #endregion

        #region Account's CommInfo Methods
        [HttpPost("{accountId}/addCommInfo")]
        public async Task<IActionResult> CreateAccountCommInfo([FromBody] CommInfoCreationDTO commInfoCreationDTO)
        {
            try
            {
                if(commInfoCreationDTO.AccountId !=null && commInfoCreationDTO.AccountId > 0) 
                {
                    var acc = await accountManager.GetAccount((int)commInfoCreationDTO.AccountId);

                    if (acc != null) 
                    {
                        var commInfo = mapper.Map<CommunicationInfo>(commInfoCreationDTO);

                        var result = await commInfoManager.CreateCommunicationInfo(commInfo);

                        if (result == true)
                        {
                            return NoContent();
                        }

                    }
                    
                }
                return BadRequest("Invalid Account Id!");

            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion
    }
}