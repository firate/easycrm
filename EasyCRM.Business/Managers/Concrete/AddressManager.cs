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

        public async Task<bool> CreateAddress(Address address)
        {
            try
            {
                if (address.AccountId > 0)
                {
                    var account = await dataContext.Accounts.AsNoTracking().Where(a => a.AccountId == address.AccountId).FirstOrDefaultAsync();
                    if (account != null)
                    {
                        dataContext.Addresses.Add(address);
                        await dataContext.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Invalid account id");
                    }
                }
                else
                {
                    dataContext.Addresses.Add(address);
                    await dataContext.SaveChangesAsync();
                    return true;
                }
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

        public async Task<bool> EditAddress(int id, Address address)
        {
            try
            {
                var adr = await dataContext.Addresses.AsNoTracking().Where(a => a.Id == id).FirstOrDefaultAsync();
                if (adr != null)
                {
                    adr = address;
                    dataContext.Addresses.Update(adr);
                    await dataContext.SaveChangesAsync();

                    return true;
                }
                else
                {
                    throw new Exception("Address not found!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Address> GetAddress(int id)
        {
            try
            {
                var address = await dataContext.Addresses.AsNoTracking().Where(a => a.Id == id).FirstOrDefaultAsync();
                if (address != null)
                {
                    return address;
                }
                else
                {
                    throw new Exception("Address not found!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
