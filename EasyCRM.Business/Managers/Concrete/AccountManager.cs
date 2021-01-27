using EasyCRM.Business.DTOs;
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

        public async Task<bool> AddAddressInfo(AddressInfoParams addressInfoParams)
        {
            try
            {
                var account = await dataContext.Accounts.Where(a => a.AccountId == addressInfoParams.AccountId).FirstOrDefaultAsync();
                var country = await dataContext.Countries.Where(c => c.Id == addressInfoParams.CountryId).FirstOrDefaultAsync();

                if (account != null)
                {
                    if (country!=null)
                    {
                        var address = new Address() 
                        { 
                            Account= account,
                            AddressTitle = addressInfoParams.AddressTitle,
                            AddressLine = addressInfoParams.AddressLine,
                            Country = country,
                            CountryId = addressInfoParams.CountryId,
                            IsMain = addressInfoParams.IsMain
                        };

                        dataContext.Add(address);
                        await dataContext.SaveChangesAsync();

                        return true;
                    }
                    
                    throw new Exception("Given CountryId is not valid");
                    
                }

                throw new Exception("Given AccountId is not valid");
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<bool> AddCommunicationInfo(CommInfoParams commInfoParams)
        {
            try
            {
                var account = await dataContext.Accounts.Where(a => a.AccountId == commInfoParams.AccountId).FirstOrDefaultAsync();
                var commType = await dataContext.CommunicationTypes.Where(c=>c.Id==commInfoParams.CommunicationTypeId).FirstOrDefaultAsync();

                if(account != null)
                {
                    var commInfo = new CommunicationInfo 
                    {
                        
                        Name=commInfoParams.Name,
                        Value=commInfoParams.Value,
                        Account=account,
                        CommunicationType= commType,
                        Description =commInfoParams.Description,
                        CreatedAt =DateTime.Now,
                        UpdatedAt =DateTime.Now
                    };

                    dataContext.CommunicationInfos.Add(commInfo);
                    await dataContext.SaveChangesAsync();

                    return true;
                }

                throw new Exception("AccountId not exists!");

            }
            catch (Exception ex)
            {
                throw;
            }
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
                    await dataContext.SaveChangesAsync();

                    return true;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<bool> DeleteAddressInfo(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditAccount(int id, AccountEditDTO accountEditDTO)
        {
            try
            {
                var account = await dataContext.Accounts.Where(a => a.AccountId == id).FirstOrDefaultAsync();

                if (account != null)
                {
                    Account editedRecord = new Account
                    {
                        AccountId=id,
                        OrganizationName=accountEditDTO.OrganizationName,
                        AccountTypeId=accountEditDTO.AccountTypeId,
                        Description=accountEditDTO.Description,
                        UpdatedAt=DateTime.Now
                    };

                    account = editedRecord;
                    dataContext.Accounts.Update(account);
                    await dataContext.SaveChangesAsync();
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

                return account;

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
