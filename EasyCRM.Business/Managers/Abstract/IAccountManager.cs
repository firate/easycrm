using EasyCRM.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCRM.Business.DTOs;
using EasyCRM.Business.ModelHelpers;
using EasyCRM.Utility;

namespace EasyCRM.Business.Managers.Abstract
{
    public interface IAccountManager
    {
        
        Task<bool> CreateAccount(Account account);
        Task<Account> GetAccount(int id);
        Task<bool> EditAccount(int id, AccountEditDTO accountEditDTO);
        Task<bool> DeleteAccount(int id);
        Task<PagedList<Account>> SearchAccounts(AccountParams accountParams);

        Task<bool> AddCommunicationInfo(CommInfoParams commInfoParams);

        Task<bool> AddAddressInfo(AddressInfoParams addressInfoParams);
        Task<bool> DeleteAddressInfo(int id);
        

        


    }
}
