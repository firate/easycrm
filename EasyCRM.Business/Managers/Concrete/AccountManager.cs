
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
                var accountType = await dataContext.AccountType.Where(at => at.Id == account.AccountTypeId).FirstOrDefaultAsync();

                if (accountType == null)
                {
                    throw new Exception("AccountType is not valid!");
                }

                else if (accountType != null)
                {
                    account.CreatedAt = DateTime.Now;
                    account.UpdatedAt = DateTime.Now;

                    await dataContext.Account.AddAsync(account);
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
                var acc = await dataContext.Account.FirstOrDefaultAsync(a => a.AccountId == id);

                if (acc == null)
                {
                    throw new Exception("No account found with given id");
                }
                else
                {
                    dataContext.Account.Remove(acc);
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
                var existingAccount = await dataContext.Account.AsNoTracking().Where(a => a.AccountId == id).Include(a=>a.AccountGroups).FirstOrDefaultAsync();
                var accountType = await dataContext.AccountType.AsNoTracking().FirstOrDefaultAsync(at => at.Id == account.AccountTypeId);

                List<AccountGroup> agList = new List<AccountGroup>();

                if(account.AccountGroups != null)
                {
                    foreach (var ag in account.AccountGroups)
                    {
                        var xGroup = await dataContext.Group.AsNoTracking().FirstOrDefaultAsync(g => g.Id == ag.GroupId);
                        if (xGroup != null)
                        {
                            ag.Account = existingAccount;
                            ag.Group = xGroup;
                            agList.Add(ag);
                        }
                    }

                    dataContext.AccountGroup.RemoveRange(existingAccount.AccountGroups);
                }
                
                if (existingAccount != null)
                {
                    existingAccount.OrganizationName = !String.IsNullOrEmpty(account.OrganizationName) ? account.OrganizationName : existingAccount.OrganizationName;
                    existingAccount.IdentificationCode = !String.IsNullOrEmpty(account.IdentificationCode) ? account.IdentificationCode : existingAccount.IdentificationCode;
                    existingAccount.VatNumber = account.VatNumber.HasValue ? account.VatNumber : existingAccount.VatNumber;
                    existingAccount.IsActive = existingAccount.IsActive == account.IsActive ? existingAccount.IsActive : account.IsActive;
                    existingAccount.Description = !String.IsNullOrEmpty(account.Description) ? account.Description : existingAccount.Description;
                    existingAccount.AccountType = accountType != null && existingAccount.AccountType == accountType ? existingAccount.AccountType : accountType;
                    existingAccount.AccountGroups = agList;

                    dataContext.Account.Update(existingAccount);

                    foreach (var accountGroup in agList)
                    {
                        var ag = await dataContext.AccountGroup.AsNoTracking().FirstOrDefaultAsync(ag => ag.AccountId == accountGroup.AccountId && ag.GroupId == accountGroup.GroupId);
                        if (ag == null)
                        {
                            dataContext.AccountGroup.Add(accountGroup);
                        }
                    }

                    return await dataContext.SaveChangesAsync()>0;
                    // return true;
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
                var account = await dataContext.Account.AsNoTracking()
                                                       .Include(a => a.AccountType)
                                                       .Include(a => a.Contacts)
                                                       .Include(a => a.AccountGroups).ThenInclude(ag => ag.Group)
                                                       .FirstOrDefaultAsync(a => a.AccountId == id);

                if (account != null)
                {
                    return account;
                }

                throw new Exception("No account found!");

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PagedList<Account>> SearchAccounts(AccountParams accountParams)
        {
            var accounts = dataContext.Account.Include(a => a.AccountType).Include(a => a.Contacts).AsQueryable();

            if (accountParams.AccountId > 0)
            {
                accounts = accounts.Where(a => a.AccountId == accountParams.AccountId);
                return await PagedList<Account>.CreateAsync(accounts, accountParams.PageNumber, accountParams.PageSize);
            }

            if (!String.IsNullOrEmpty(accountParams.OrganizationName))
            {
                accounts = accounts.Where(a => a.OrganizationName.Contains(accountParams.OrganizationName));
            }

            if (!String.IsNullOrEmpty(accountParams.AccountType))
            {
                accounts = accounts.Where(a => a.AccountType.Name.Contains(accountParams.AccountType));
            }

            if (!String.IsNullOrEmpty(accountParams.Description))
            {
                accounts = accounts.Where(a => a.Description.Contains(accountParams.Description)).OrderByDescending(a => a.AccountId);
            }

            accounts = accounts.OrderByDescending(a => a.AccountId);

            return await PagedList<Account>.CreateAsync(accounts, accountParams.PageNumber, accountParams.PageSize);

        }
    }
}
