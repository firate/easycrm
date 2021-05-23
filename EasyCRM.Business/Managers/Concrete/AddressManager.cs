using EasyCRM.Business.Managers.Abstract;
using EasyCRM.Business.ModelHelpers;
using EasyCRM.Data.EF;
using EasyCRM.Entity.Models;
using EasyCRM.Utility;
//using EasyCRM.Utility;
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
                    var account = await dataContext.Account.AsNoTracking().Where(a => a.AccountId == address.AccountId).FirstOrDefaultAsync();
                    if (account != null)
                    {
                        dataContext.Address.Add(address);
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
                    dataContext.Address.Add(address);
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
                var address = await dataContext.Address.AsNoTracking().Where(a => a.Id == id).FirstOrDefaultAsync();

                if (address != null && address.IsMain != true)
                {
                    dataContext.Address.Remove(address);
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
                var adr = await dataContext.Address.AsNoTracking().Where(a => a.Id == id).FirstOrDefaultAsync();
                if (adr != null)
                {
                    adr = address;
                    dataContext.Address.Update(adr);
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
                var address = await dataContext.Address.AsNoTracking().Where(a => a.Id == id).FirstOrDefaultAsync();
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

        public async Task<Address> GetAddress(int id, int accountId)
        {
            try
            {
                var address = await dataContext.Address.AsNoTracking()
                                                .Where(a => a.Id == id)
                                                .Where(a => a.AccountId == accountId)
                                                .Include(a => a.Account)
                                                .Include(a => a.Country)
                                                .FirstOrDefaultAsync();
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

        public Task<Address> GetAddress(int id, int contactId, int? accountId = null)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<Address>> SearchAddresses(AddressParams addressParams)
        {
            var addresses = dataContext.Address.Include(a => a.Account)
                                              .Include(a => a.Country)
                                              .Include(a => a.ContactAddresses).ThenInclude(c => c.Contact)
                                              .AsQueryable();

            if(addressParams.Id > 0)
            {
                addresses = addresses.Where(a=>a.Id==addressParams.Id);
                return await PagedList<Address>.CreateAsync(addresses,addressParams.PageNumber,addressParams.PageSize);
            }

            if (!String.IsNullOrEmpty(addressParams.AddressTitle))
            {
                addresses = addresses.Where(a => a.AddressTitle.Contains(addressParams.AddressTitle));
            }

            if (!String.IsNullOrEmpty(addressParams.AddressLine))
            {
                addresses = addresses.Where(a => a.AddressLine.Contains(addressParams.AddressLine));
            }

            if (!String.IsNullOrEmpty(addressParams.State))
            {
                addresses = addresses.Where(a => a.State.Contains(addressParams.State));
            }

            if (!String.IsNullOrEmpty(addressParams.PostalCode))
            {
                addresses = addresses.Where(a => a.PostalCode.Contains(addressParams.PostalCode));
            }

            if (addressParams.AccountId !=null && addressParams.AccountId >0 )
            {
                addresses = addresses.Where(a => a.AccountId==addressParams.AccountId);
            }

            if (addressParams.CountryId > 0)
            {
                addresses = addresses.Where(a => a.CountryId == addressParams.CountryId);
            }

            if (addressParams.ContactId > 0)
            {
                addresses = addresses.Include(a=>a.ContactAddresses).ThenInclude(ca=>ca.ContactId==addressParams.ContactId);
            }

            if(addressParams.IsMain != null)
            {
                addresses = addresses.Where(a => a.IsMain == addressParams.IsMain);
            }

            return await PagedList<Address>.CreateAsync(addresses,addressParams.PageNumber, addressParams.PageSize);
        }
    }
}
