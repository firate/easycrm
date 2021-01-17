
using Microsoft.AspNetCore.Mvc;
using EasyCRM.Business.Managers.Abstract;
using AutoMapper;

using System.Threading.Tasks;
using System;
using EasyCRM.Entity.Models;
using EasyCRM.API.DTOs;

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

        //[HttpGet]
        //public async Task<IActionResult> SearchAccounts([FromQuery] AccountParams accountParams)
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