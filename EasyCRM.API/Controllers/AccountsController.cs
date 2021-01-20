
using Microsoft.AspNetCore.Mvc;
using EasyCRM.Business.Managers.Abstract;
using AutoMapper;

using System.Threading.Tasks;
using System;
using EasyCRM.Entity.Models;
using EasyCRM.API.DTOs;
using EasyCRM.Utility;
using System.Collections.Generic;

namespace EasyCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManager accountManager;
        private readonly IMapper mapper;


        public AccountsController(IAccountManager _accountManager,IMapper _mapper)
        {
            accountManager = _accountManager;
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

        //[HttpPut("{id}")]
        //public async Task<IActionResult> EditAccountInfo(int id, [FromBody] AccountEditDTO accountEditDTO)
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

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAccount(int id)
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