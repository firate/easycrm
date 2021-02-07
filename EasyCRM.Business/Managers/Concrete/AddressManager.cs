using EasyCRM.Business.Managers.Abstract;
using EasyCRM.Data.EF;
using EasyCRM.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Business.Managers.Concrete
{
    public class AddressManager : IAddressManager
    {
        
        private readonly DataContext dataContext;

        public AddressManager(DataContext _dataContext)
        {
            dataContext = _dataContext;
        }

        public async Task<bool> CreateAddress(Address address, int? accountId, int? contactId)
        {
            try
            {
                var account = await dataContext.Accounts.AsNoTracking().Where(a => a.AccountId == accountId).FirstOrDefaultAsync();

                if(address != null)
                {
                    if(account != null)
                    {
                        address.AccountId = account.AccountId;
                    }

                    dataContext.Addresses.Add(address);
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

        public async Task<bool> DeleteAddress(int id)
        {
            try
            {
                var address = await dataContext.Addresses.AsNoTracking().Where(a => a.Id == id).FirstOrDefaultAsync();

                if (address != null)
                {
                    dataContext.Addresses.Remove(address);
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

        public Task<bool> EditAddress(int id, Address address)
        {
            throw new NotImplementedException();
        }

        public Task<Address> GetAddress(int id)
        {
            throw new NotImplementedException();
        }
    }
}
