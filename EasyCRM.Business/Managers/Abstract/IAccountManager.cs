using EasyCRM.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Business.Managers.Abstract
{
    public interface IAccountManager
    {
        
        Task<bool> CreateAccount(Account account);
        Task<Account> GetAccount(int id);
        Task<bool> EditManager(Account account);
        Task<bool> DeleteManager(Account account);
        Task<List<Account>> GetAccounts();
        Task<List<Account>> GetAccountsByName();
 
        
    }
}
