
using EasyCRM.Business.Managers.Abstract;
using EasyCRM.Business.ModelHelpers;
using EasyCRM.Data.EF;
using EasyCRM.Entity.Models;
using EasyCRM.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Business.Managers.Concrete
{
    public class AccountManager : IAccountManager
    {
        private readonly DataContext dataContext;

        public AccountManager(DataContext _dataContext)
        {
            dataContext = _dataContext;
        }

        public async Task<bool> CreateAccount(Account account)
        {
            try
            {
                var accountType = await dataContext.AccountTypes.Where(at => at.Id == account.AccountTypeId).FirstOrDefaultAsync();
                
                if(accountType == null)
                {
                    throw new Exception("AccountType is not valid!");
                }

                else if(accountType != null) 
                {
                    await dataContext.Accounts.AddAsync(account);
                    var result = await dataContext.SaveChangesAsync();

                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
                
                return false;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<bool> DeleteAccount(int id)
        {
            try
            {
                var acc =await dataContext.Accounts.FirstOrDefaultAsync(a=>a.AccountId==id);

                if(acc == null)
                {
                    throw new Exception("No account found with given id");
                }
                else
                {
                    dataContext.Accounts.Remove(acc);
                    var result = await dataContext.SaveChangesAsync();

                    if (result > 0)
                    {
                        return true;
                    }

                    return false;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> EditAccount(int id, Account account)
        {
            try
            {
                
                var acc =await dataContext.Accounts.AsNoTracking().Where(a => a.AccountId == id).FirstOrDefaultAsync();

                if (acc != null)
                {
                    account.AccountId = id;
                    dataContext.Accounts.Update(account);
                    await dataContext.SaveChangesAsync();
                    return true;
                }
                return false;
                
               
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Account> GetAccount(int id)
        {
            try
            {
                var account = await dataContext.Accounts.Include(a => a.AccountType).Include(a=>a.Contacts).FirstAsync(a => a.AccountId == id);
                if (account != null)
                {
                    return account;
                }
                else
                {
                    throw new Exception("Account not found!");
                }
                

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<PagedList<Account>> SearchAccounts(AccountParams accountParams)
        {
            var accounts = dataContext.Accounts.Include(a=>a.AccountType).Include(a=>a.Contacts).AsQueryable();

            if(accountParams.AccountId > 0)
            {
                accounts = accounts.Where(a=>a.AccountId==accountParams.AccountId);
                return await PagedList<Account>.CreateAsync(accounts, accountParams.PageNumber,accountParams.PageSize);
            }

            if (!String.IsNullOrEmpty(accountParams.OrganizationName))
            {
                accounts= accounts.Where(a => a.OrganizationName.Contains(accountParams.OrganizationName));
            }

            if (!String.IsNullOrEmpty(accountParams.AccountType))
            {
                accounts = accounts.Where(a => a.AccountType.Name.Contains(accountParams.AccountType));
            }

            if (!String.IsNullOrEmpty(accountParams.Description))
            {
                accounts = accounts.Where(a=>a.Description.Contains(accountParams.Description));
            }

            return await PagedList<Account>.CreateAsync(accounts,accountParams.PageNumber, accountParams.PageSize);

        }
    }
}
